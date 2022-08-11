<template>
  <div class="container">
    <div class="main">
      <!-- 整个注册盒子 -->
      <div class="loginbox">
        <!-- 左侧的注册盒子 -->
        <div class="loginbox-in">
          <div class="userbox">
            <v-icon class="iconfont">
              mdi-account
            </v-icon>
            <input
              id="user"
              v-model="id"
              class="user"
              placeholder="Id"
            >
          </div>
          <br>
          <div class="pwdbox">
            <v-icon class="iconfont">
              mdi-key
            </v-icon>
            <!-- <span class="iconfont">&#xe775;</span> -->
            <input
              id="password"
              v-model="pwd"
              class="pwd"
              type="password"
              placeholder="password"
            >
          </div>
          <br>
          <div
            class="log-box"
            style="text-align:right"
          >
            <button
              type="primary"
              class="login_btn"
              @click="login"
            >
              Login
            </button>
          </div>
          <br>
          <button
            type="primary"
            class="register_btn"
            @click="register"
          >
            注册账号
          </button>
        </div>

        <!-- 右侧的注册盒子 -->
        <div class="background">
          <div class="novel">
            医院管理系统
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
  import axios from 'axios'
  import { sync } from 'vuex-pathify'
  export default {
    name: 'Login',

    data: function () {
      return {
        id: '',
        pwd: '',
      }
    },
    computed: {
      jwt: sync('app/jwt'),
    },
    methods: {
      register () { this.$router.push('/Register') },
      login () {
        const outerthis = this
        // console.log({
        //   Id: Number(this.id),
        //   Password: this.pwd,
        // })
        if (this.id[0] === '1') { // 病人
          axios.post('/auth/login_patient', {
            Id: Number(this.id),
            Password: this.pwd,
          })
            .then(function (response) {
              outerthis.jwt = response.data
              outerthis.$router.push({ name: 'Dashboard' })
              console.log(response)
            })
            .catch(function (error) {
              if (error.response.status === 400) {
                alert(error.response.data)
              } else {
                alert(error.message)
              }
              console.log(error)
            })
        } else { // 员工
          axios.post('/auth/login_staff', {
            Id: Number(this.id),
            Password: this.pwd,
          })
            .then(function (response) {
              outerthis.jwt = response.data
              outerthis.$router.push({ name: 'Dashboard' })
              console.log(response)
            })
            .catch(function (error) {
              if (error.response.status === 400) {
                alert(error.response.data)
              } else {
                alert(error.message)
              }
              console.log(error)
            })
        }
      },
    },
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
    width:800px;
    height:400px;
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
    margin-top:120px ;
    height:30px;
     width:230px;
     display: flex;
     margin-left:25px;
}
.pwdbox{
    height:30px;
    width:225px;
    display: flex;
    margin-left:25px;
}

.background{
    width:570px;
    background-image:url('../assets/Christmas_Trees.png');
    background-size:cover;
    font-family:sans-serif;
}
.novel{
    margin-top:320px;
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
  /* 修改默认背景框的颜色 */
 box-shadow: 0 0 0px 1000px  #89AB9E inset !important;
 /* 修改默认字体的颜色 */
 -webkit-text-fill-color: #445b53;
}

input:-webkit-autofill::first-line {
  /* 修改默认字体的大小 */
 font-size: 15px;
 /* 修改默认字体的样式 */
 font-weight:bold;
 }
.log-box{
    font-size:12px;
    display: flex;
    justify-content: space-between ;
    width:190px;
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
.login_btn{
    background-color: #5f8276; /* Green */
    border: none;
    color: #FAFAFA;
    padding: 8px 30px;
    text-align: center;
    text-decoration: none;
    font-size: 13px;
    border-radius: 20px;
    outline:none;
}
.login_btn:hover{
    box-shadow: 0 12px 16px 0 rgba(0,0,0,0.24), 0 17px 50px 0 rgba(0,0,0,0.19);
    cursor: pointer;
     background-color: #0b5137;
      transition: all 0.2s ease-in;
}

.warn{
    margin-top:60px;
    /* margin-right:120px; */
    margin-left:-120px;
    margin-bottom: 5px;
     font-weight:bold;
    font-size:17px;
}

.register_btn{
    background-color: transparent; /* Green */
    border: none;
    text-decoration: none;
    font-size: 12px;
    /* border-radius: 20px;   */
     color:#4E655D;
    font-size:12px;
    text-decoration:underline;
    display: flex;
    margin-left:25px;
    outline:none;

}
.register_btn:hover{
    font-weight:bold;
    cursor: pointer;
}

</style>
