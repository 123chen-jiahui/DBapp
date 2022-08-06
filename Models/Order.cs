using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Stateless;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Models
{
    // 订单状态
    public enum OrderStateEnum
    {
        Pending, // 订单已生成
        Processing, // 支付处理中
        Completed, // 交易成功
        Declined, // 交易失败
        Cancelled, // 订单取消
        Refund, // 已退款
    }

    // 触发动作
    public enum OrderStateTriggerEnum
    {
        PlaceOrder, // 支付
        Approve, // 收款成功
        Reject, // 收款失败
        Cancel, // 取消
        Return // 退货
    }


    [Table("ORDERS")]
    public class Order
    {
        // 构建函数的作用：每次创建订单的时候都会初始化订单的状态机
        public Order()
        {
            StateMachineInit();
        }

        [Key]
        [Required]
        [Column("ID")]
        public Guid Id { get; set; }
        [Required]
        [Column("PATIENT_ID")]
        public int PatientId { get; set; }
        [ForeignKey("PatientId")]
        public Patient Patient { get; set; } // 并不会到数据库中，而是由EF保存，表示外键关系
        public ICollection<LineItem> OrderItems { get; set; } // 保存商品（药品）列表
        [Required]
        [Column("STATE")]
        public OrderStateEnum State { get; set; }
        [Required]
        [Column("CREATE_DATE_UTC")]
        public DateTime CreateDateUTC { get; set; } // 支付时间
        [Required]
        [Column("TRANSACTION_METADATA")]
        public string TransactionMetadata { get; set; } // 第三方支付信息

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
