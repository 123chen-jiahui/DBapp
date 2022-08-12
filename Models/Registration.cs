using Stateless;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Models
{
    // 注意该model的名称可能会产生歧义，这里的registration表示挂号，而不是zhuce
    [Table("REGISTRATIONS")]
    public class Registration
    {
        // 构建函数的作用：每次创建订单的时候都会初始化订单的状态机
        public Registration()
        {
            StateMachineInit();
        }


        [Key]
        [Required]
        [Column("ID")]
        public Guid Id { get; set; }
        [Required]
        [Column("TIME")]
        public DateTime Time { get; set; } // 计算得来

        [Column("DAY")]
        public int Day { get; set; }
        [Column("ORDER")]
        public int Order { get; set; } // 看病序号
        
        [Required]
        [Column("FEE", TypeName = "decimal(18, 2)")]
        public decimal fee { get; set; }
        [Required]
        [Column("PATIENT_ID")]
        public int PatientId { get; set; }
        [Required]
        [Column("STAFF_ID")]
        public int StaffId { get; set; }

        [Column("ROOM_ID")]
        [Required]
        public string RoomId { get; set; }
        [ForeignKey("RoomId")]
        public Room Room { get; set; }
        // 我觉得还需要加入地点，否则病人找不到看病位置
        // 当然地点应该是由医生的信息自动生成的

        [Column("STATE")]
        [Required]
        public OrderStateEnum State { get; set; }

        [Column("CREATE_DATE_LOCAL")]
        public DateTime CreateDateLocal { get; set; }

        [Required]
        [Column("TRANSACTION_METADATA")]
        public string TransactionMetadata { get; set; } // 第三方支付信息

        [ForeignKey("PatientId")]
        public Patient Patient { get; set; }
        [ForeignKey("StaffId")]
        public Staff Staff { get; set; }


        StateMachine<OrderStateEnum, OrderStateTriggerEnum> _machine;


        // 处理下单的函数
        public void PaymentProcessing() // 要转化的状态
        {
            _machine.Fire(OrderStateTriggerEnum.PlaceOrder); // 触发动作
        }

        public void PaymentApprove()
        {
            _machine.Fire(OrderStateTriggerEnum.Approve);
        }

        public void PaymentReject()
        {
            _machine.Fire(OrderStateTriggerEnum.Reject);
        }

        // 初始化状态机
        private void StateMachineInit()
        {
            _machine = new StateMachine<OrderStateEnum, OrderStateTriggerEnum>
                (() => State, s => State = s);

            _machine.Configure(OrderStateEnum.Pending)
                .Permit(OrderStateTriggerEnum.PlaceOrder, OrderStateEnum.Processing)
                .Permit(OrderStateTriggerEnum.Cancel, OrderStateEnum.Cancelled);

            _machine.Configure(OrderStateEnum.Processing)
                .Permit(OrderStateTriggerEnum.Approve, OrderStateEnum.Completed)
                .Permit(OrderStateTriggerEnum.Reject, OrderStateEnum.Declined);

            _machine.Configure(OrderStateEnum.Declined)
                .Permit(OrderStateTriggerEnum.PlaceOrder, OrderStateEnum.Processing);

            _machine.Configure(OrderStateEnum.Completed)
                .Permit(OrderStateTriggerEnum.Return, OrderStateEnum.Refund);
        }
    }
}
