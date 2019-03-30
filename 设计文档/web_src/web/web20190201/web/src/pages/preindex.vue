<template>
  <div class="loginCenter">
    <div class="login-con">
        <img src="/static/image/datasummary.png" @click="handleClick(1)" alt="">
        <img src="/static/image/infosubmit.png" @click="handleClick(2)" alt="">
        <img src="/static/image/datasearch.png" @click="handleClick(3)" alt="">
        <img v-if="roleID==1||role==3" src="/static/image/contentmanage.png" @click="handleClick(4)" alt="">
    </div>
  </div>

</template>

<script>
  import crypto from 'crypto'
  // add by lwj---2018.12.16---接口
  import { loginFun } from '@/api/login'
  // 引入cookie
  import Cookies from 'js-cookie'
  export default {
    name: "login",
    data(){
        return{
          // const validePass = (rule, value, callback) => {
          //   var pass = /^1[3-9][0-9]{9}$/;
          //   if (!pass.test(value)) {
          //     callback(new Error('请输入正确格式的密码'));
          //   } else {
          //     callback();
          //   }
          // },
          form: {
            userName: '',
            password: ''
          },
          rules: {
            userName: [
              { required: true, message: '账号不能为空', trigger: 'blur' }
            ],
            password: [
              { required: true, message: '密码不能为空', trigger: 'blur' },
              { type: 'string', min: 6, message: '密码长度不能小于6位', trigger: 'blur' },
              // {validator:validePass}
            ]
          },
          roleID:Cookies.get('RoleID')
        }
    },
    methods: {
      // onlogin(){
      //   // debugger
      //   this.$router.push("/HomePages")
      // }
        handleClick(type){
            debugger;
            if(type == 1){
                this.$router.push("/HomePages");
            }else if(type == 2){
                this.$router.push("/Mains/LeaderList");
            }else if(type == 3){
                this.$router.push("/Mains/IntQuery");    
            }else if(type == 4){
                this.$router.push("/Mains/DicManaged");
            }
        }
    }
  }
</script>

<style scoped>
  .loginCenter{
    width: 100%;
    height: 100%;
    /* background: url(../assets/image/timg.jpg) no-repeat 0 0; */
    background: url(/static/image/bg_new_5.jpg) no-repeat 0 0;
    background-size: 100% 100%;
    position: relative;
    display: flex;
    justify-content: center;
  }
  .login-con {
    position: absolute;
    /* right: 160px; */
    top: 60%;
    -webkit-transform: translateY(-60%);
    transform: translateY(-60%);
    width: 100%;
    text-align: center;
  }
    .login-con img{
        width: 250px;
        margin: 0 20px;
        cursor: pointer;
    }


</style>
