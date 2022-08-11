<template>
  <div class="container">
    <div class="main">
      <!-- 整个注册盒子 -->
      <div class="loginbox">
        <!-- 左侧的注册盒子 -->
        <div class="loginbox-in">
          <div v-if="patient">
            <br><br>
          </div>
          <div style="text-align:center">
            <button
              type="primary"
              class="register_btn"
              @click="patient=!patient"
            >
              {{ patient?'病人注册':'员工注册' }}
            </button>
          </div>
          <div class="userbox">
            <v-icon class="iconfont">
              mdi-account
            </v-icon>
            <input
              v-model="ID"
              class="user"
              placeholder="身份证号"
            >
          </div>
          <div class="pwdbox">
            <v-icon class="iconfont">
              mdi-account
            </v-icon>
            <input
              v-model="name"
              class="user"
              placeholder="姓名"
            >
          </div>
          <div class="pwdbox">
            <v-icon class="iconfont">
              mdi-account
            </v-icon>
            <input
              v-model="gender"
              type="radio"
              value="1"
            ><label class="gend">男</label>
            <input
              v-model="gender"
              type="radio"
              value="0"
            ><label class="gend">女</label>
          </div>
          <div class="pwdbox">
            <v-icon class="iconfont">
              mdi-account
            </v-icon>
            <input
              v-model="birth"
              class="user"
              placeholder="生日(1900-01-01)"
            >
          </div>
          <div class="pwdbox">
            <v-icon class="iconfont">
              mdi-account
            </v-icon>
            <input
              v-model="phone"
              class="user"
              placeholder="电话号码"
            >
          </div>
          <div
            v-if="!patient"
            class="pwdbox"
          >
            <v-icon class="iconfont">
              mdi-account
            </v-icon>
            <input
              v-model="role"
              class="user"
              placeholder="职务(0-2)"
            >
          </div>
          <div
            v-if="!patient"
            class="pwdbox"
          >
            <v-icon class="iconfont">
              mdi-account
            </v-icon>
            <input
              v-model="depart"
              class="user"
              placeholder="部门(1-10)"
            >
          </div>
          <div
            v-if="!patient"
            class="pwdbox"
          >
            <v-icon class="iconfont">
              mdi-account
            </v-icon>
            <input
              v-model="add"
              class="user"
              placeholder="地址"
            >
          </div>
          <div class="pwdbox">
            <v-icon class="iconfont">
              mdi-key
            </v-icon>
            <input
              v-model="pwd"
              class="pwd"
              type="password"
              placeholder="密码"
            >
          </div>
          <div class="pwdbox">
            <v-icon class="iconfont">
              mdi-key
            </v-icon>
            <input
              v-model="conpwd"
              class="pwd"
              type="password"
              placeholder="确认密码"
            >
          </div>
          <div style="text-align:center">
            <button
              type="primary"
              class="register_btn"
              @click="register"
            >
              注册
            </button>
          </div>
        </div>

        <!-- 右侧的注册盒子 -->
        <div class="background">
          <div class="novel">
            医 院 管 理 系 统 注 册
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
  import axios from 'axios'
  export default {
    name: 'Register',

    data: function () {
      return {
        ID: '',
        name: '',
        gender: '',
        birth: '',
        phone: '',
        pwd: '',
        conpwd: '',
        patient: 1,
        role: '',
        depart: '',
        add: '',
      }
    },
    methods: {

      register () {
        this.role = Number(this.role)
        this.depart = Number(this.depart)
        this.gender = Number(this.gender)
        if (!identityIDCard(this.ID)) {
          alert('无效的身份证号')
          return
        }
        if (!isPoneAvailable(this.phone)) {
          alert('无效的电话号码')
          return
        }
        if (this.gender !== 1 && this.gender !== 0) {
          alert('未选择性别')
          return
        }
        if (!this.patient) {
          if (this.role > 2 || this.role < 0) {
            alert('无效的职务')
            return
          }
          if (this.depart > 10 || this.depart < 1) {
            alert('无效的部门')
            return
          }
        }
        if (this.pwd !== this.conpwd) {
          alert('密码和确认密码不一致')
          return
        }
        if (this.patient) {
          axios.post('/auth/register_patient', {
            GlobalId: this.ID,
            Password: this.pwd,
            ConfirmPassword: this.conpwd,
            Name: this.name,
            Gender: this.gender,
            Birthday: this.birth,
            Phone: this.phone,
          })
            .then(function (response) {
              let alt = '以下是您的账号，请务必牢记：\n'
              alt = alt + response.data
              alert(alt)
              console.log(response)
              this.$router.push({ name: 'Login' })
            })
            .catch(function (error) {
              alert(error.message)
              console.log(error)
            })
        } else { // 员工
          axios.post('/auth/register_staff', {
            GlobalId: this.ID,
            Password: this.pwd,
            ConfirmPassword: this.conpwd,
            Role: this.role,
            Name: this.name,
            Gender: this.gender,
            Birthday: this.birth,
            Address: this.add,
            Phone: this.phone,
            Department: this.depart,
          })
            .then(function (response) {
              let alt = '以下是您的账号，请务必牢记：\n'
              alt = alt + response.data
              alert(alt)
              console.log(response)
              this.$router.push({ name: 'Login' })
            })
            .catch(function (error) {
              alert(error.message)
              console.log(error)
            })
        }
      },
    },
  }

  function identityIDCard (code) {
    // 身份证号前两位代表区域
    const city = {
      11: '北京',
      12: '天津',
      13: '河北',
      14: '山西',
      15: '内蒙古',
      21: '辽宁',
      22: '吉林',
      23: '黑龙江 ',
      31: '上海',
      32: '江苏',
      33: '浙江',
      34: '安徽',
      35: '福建',
      36: '江西',
      37: '山东',
      41: '河南',
      42: '湖北 ',
      43: '湖南',
      44: '广东',
      45: '广西',
      46: '海南',
      50: '重庆',
      51: '四川',
      52: '贵州',
      53: '云南',
      54: '西藏 ',
      61: '陕西',
      62: '甘肃',
      63: '青海',
      64: '宁夏',
      65: '新疆',
      71: '台湾',
      81: '香港',
      82: '澳门',
      91: '国外 ',
    }
    const idCardReg = /^[1-9]\d{5}(19|20)?\d{2}(0[1-9]|1[012])(0[1-9]|[12]\d|3[01])\d{3}(\d|X)$/i // 身份证格式正则表达式
    let isPass = true // 身份证验证是否通过（true通过、false未通过）

    // 如果身份证不满足格式正则表达式
    if (!code) {
      isPass = false
    } else if (!code.match(idCardReg)) {
      isPass = false
    } else if (!city[code.substr(0, 2)]) {
      // 区域数组中不包含需验证的身份证前两位
      isPass = false
    } else if (code.length === 18) {
      // 18位身份证需要验证最后一位校验位
      code = code.split('')
      // ∑(ai×Wi)(mod 11)
      // 加权因子
      const factor = [7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2]
      // 校验位
      const parity = [1, 0, 'X', 9, 8, 7, 6, 5, 4, 3, 2]
      let sum = 0
      let ai = 0
      let wi = 0
      for (let i = 0; i < 17; i++) {
        ai = parseInt(code[i])
        wi = factor[i]
        sum += ai * wi // 开始计算并相加
      }
      const last = parity[sum % 11] // 求余
      if (last.toString() !== code[17]) {
        isPass = false
      }
    }
    return isPass
  }

  function isPoneAvailable (phone) {
    const myreg = /^[1][3,4,5,7,8][0-9]{9}$/
    if (!myreg.test(phone)) {
      return false
    } else {
      return true
    }
  }
</script>

<style scoped>
.iconfont {
    font-family: "iconfont" !important;
    font-size: 20px;
    font-style: normal;
    -webkit-font-smoothing: antialiased;
    -moz-osx-font-smoothing: grayscale;
    height:22px;
    color:#4E655D;
    margin-right:10px;
    margin-top:3px;
}
.loginbox{
    display:flex;
    position:absolute;
    width:1000px;
    height:500px;
    top:40%;
    left:50%;
    transform:translate(-50%,-50%);
    box-shadow: 0 12px 16px 0  rgba(0,0,0,0.24), 0 17px 50px 0 #4E655D;
}
.loginbox-in{
     background-color:#89AB9E;
     width:240px;
}
.userbox{
    margin-top:5px ;
    height:30px;
     width:230px;
     display: flex;
     margin-left:25px;
}
.pwdbox{
  margin-top:10px;
    height:30px;
    width:225px;
    display: flex;
    margin-left:25px;
}

.background{
    width:760px;
    background-image:url('../assets/Christmas_Trees.png');
    background-size:cover;
    font-family:sans-serif;
}
.novel{
    margin-top:400px;
    font-weight:bold;
    font-size:20px;
    color:#4E655D;
    text-align: center;

}
.novel:hover{
     font-size:21px;
     transition: all 0.4s ease-in-out;
     cursor: pointer;
}
.uesr-text{
     position:left;
}
input{
    outline-style: none ;
    border: 0;
    border-bottom:1px solid #E9E9E9;
    background-color:transparent;
    height:20px;
     font-family:sans-serif;
    font-size:15px;
    color:#445b53;
    font-weight:bold;
}
 /* input::-webkit-input-placeholder{
    color:#E9E9E9;
 } */
input:focus{
    border-bottom:2px solid #445b53;
    background-color:transparent;
     transition: all 0.2s ease-in;
     font-family:sans-serif;
    font-size:15px;
     color:#445b53;
     font-weight:bold;

}
input:hover{
    border-bottom:2px solid #445b53;
    background-color:transparent;
     transition: all 0.2s ease-in;
     font-family:sans-serif;
    font-size:15px;
     color:#445b53;
     font-weight:bold;

}

input:-webkit-autofill {
 box-shadow: 0 0 0px 1000px  #89AB9E inset !important;
 -webkit-text-fill-color: #445b53;

}

input:-webkit-autofill::first-line {
 font-size: 15px;
 font-weight:bold;
 }
.log-box{
    font-size:12px;
    display: flex;
    justify-content: space-between ;
    width:174px;
    margin-left:30px;
    color:#4E655D;
    margin-top:-5px;
    align-items: center;

}
.log-box-text{
    color:#4E655D;
    font-size:12px;
      text-decoration:underline;
    }
.register_btn{
  margin-top:10px;
    background-color: #5f8276; /* Green */
    border: none;
    color: #FAFAFA;
    padding: 7px 35px;
    text-align: center;
    text-decoration: none;
    font-size: 13px;
    border-radius: 20px;
    outline:none;
}
.register_btn:hover{
    box-shadow: 0 12px 16px 0 rgba(0,0,0,0.24), 0 17px 50px 0 rgba(0,0,0,0.19);
    cursor: pointer;
     background-color: #0b5137;
    transition: all 0.2s ease-in;
}

.warn{
    margin-top:60px;
    margin-right:110px;
    margin-bottom: 5px;
    font-weight:bold;
    font-size:17px;
}

.register_btn:hover{
    font-weight:bold;
    cursor: pointer;
}
.gend{
  color: #0b5137;
font-size: 18px;
padding: 0px 15px;
}
</style>
