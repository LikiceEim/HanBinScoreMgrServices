<template>
  <div class="headers">
   <div class="headerTop">
     <h3 style="margin-left:20px;">汉滨区科级领导干部积分管理考核系统</h3>
     <!-- <div class="system" @mouseenter="enterBtn" @mouseleave="leaveBtn"> -->
    <div class="system">
        <Dropdown trigger="click" @on-click="handleClick">
          <DropdownMenu slot="list">
            <DropdownItem name="editPWD">修改密码</DropdownItem>
            <DropdownItem name="logout">退出登录</DropdownItem>
        </DropdownMenu>
          <p>{{name}}</p>
          <div class="iv-icons">
            <Icon class="iv-icon" type="md-arrow-dropdown" size="24"  style="color:#2d8cf0"/>
            <img src="../assets/image/icos1.gif" alt="">
          </div>
          <div class="systenHover" v-show="show==true" @mouseenter="enterBtn()" @mouseleave="leaveBtn()">
            <ul>
              <!--<li  :class="red" v-for="list in lis"  @mouseenter="enterBtn(list)" @mouseleave="leaveBtn(list)">{{list.name}}</li>-->
              <!-- <li >修改密码</li> -->
              <router-link to="/Login" tag="li">退出登录</router-link>
            </ul>
          </div>
        </Dropdown>
     </div>
    <!-- 修改密码框 -->
    <Modal
        v-model="isEditPWD"
        title="编辑密码"
        width="420"
        height="300"
        class-name="vertical-center-modal">
        <div style="margin-top:15px;">
          <span style="display:inline-block;width:120px;text-align:right;padding-right:30px;">原密码</span>
          <Input type="password" style="width:220px;" placeholder="请输入原密码" v-model="oldPwd"></Input>
        </div>
        <div style="margin-top:15px;">
          <span style="display:inline-block;width:120px;text-align:right;padding-right:30px;">新密码</span>
          <Input type="password" style="width:220px;" placeholder="请输入新密码" v-model="newPwd"></Input>
        </div>
        <div style="margin-top:15px;">
          <span style="display:inline-block;width:120px;text-align:right;padding-right:30px;">确认新密码</span>
          <Input type="password" style="width:220px;" placeholder="确认新密码" v-model="confirmNewPwd"></Input>
        </div>
        <div slot="footer" style="text-align: center">
          <Button type="primary" @click="doEditPWD">确定</Button>
        </div>
    </Modal>
    <!-- 修改密码框 end -->
   </div>


  </div>
</template>

<script>
  import Cookies from 'js-cookie';
  import {UpdatePWD} from '@/api/usersManage';
  // 引入base64
  import base64 from 'js-base64';
    export default {
        name: "headers",
      data(){
          return{
            // 编辑密码
            oldPwd:null,
            newPwd:null,
            confirmNewPwd:null,
            isEditPWD:false,
            isred:true,
            show:false,
            name: null,
          }
      },
      created(){
        debugger;
        this.name = Cookies.get('UserName');
      },
      methods:{
        // 执行编辑密码
      doEditPWD(){
        debugger;
        var CurrentUserID = Cookies.get('UserID');
        var OriginPWDval = this.oldPwd;
        var NewPWDval = this.newPwd;
        var confirmNewPwd = this.confirmNewPwd;
        if(OriginPWDval == undefined || NewPWDval == undefined || confirmNewPwd == undefined){
          this.$Message.info('请输入完整信息！')
          return false;
        }
        // 判断如果两个密码不一致，则提示
        if(NewPWDval != confirmNewPwd){
          this.$Message.info('您输入的两次密码不一致！');
          // this.isEditPWD = true;
          return false;
        }
        var OriginPWD = Base64.encode(OriginPWDval);
        var NewPWD = Base64.encode(NewPWDval);
        var _data = {
          CurrentUserID,
          OriginPWD,
          NewPWD
        }
        UpdatePWD(_data).then(res => {
          debugger;
          if(res.IsSuccessful == true){
            this.$Message.success('修改密码成功！');
            this.isEditPWD = false;
          }else{
            this.$Message.error(res.Reason);
          }
        })
      },
        // 点击推出
        handleClick(val){
          debugger;
          if(val == 'logout'){
            Cookies.set('token',null);
            this.$router.push("/login");
          }
          // add by lwj---修改密码---2019.01.18
          else if(val == 'editPWD'){
            this.isEditPWD = true;
            this.oldPwd = null;
            this.newPwd = null;
            this.confirmNewPwd = null;
          }
        },
        enterBtn(){
          // debugger;
          this.show=true;

        },leaveBtn(){
          this.show=false;
        },
      }
    }
</script>

<style scoped>
  .headers{
    height:100%;
  }
  .headerTop{
    width:100%;
    height:100%;
    background: #fff;
    display: -webkit-flex;
    justify-content: space-between;
    padding:0 65px 0 0;
    box-sizing: border-box;
    align-items: center;
  }
  .headerTop h3{
    color: #ff6c60;
    font-size: 28px;
  }
  .system{
    height: 60px;
    border-radius:5px;
    display:-webkit-flex;
    justify-content: space-around;
    align-items: center;
    cursor:pointer;
  }
  .system p{
    font-size: 12px;
    color: #2d8cf0;
  }
  .system .iv-icons{
    font-size: 12px;
    color: black;
    display: -webkit-flex;
    -webkit-align-items: center;
  }
  .system .iv-icons img{
    display: block;
    border-radius: 50%;
  }
  .system .iv-icons .iv-icon{
    display: block;
  }

  .systenHover{
    height: 90px;
    line-height: 90px;
    display: -webkit-flex;
    justify-content: center;
    -webkit-align-items: center;
    flex-direction: column;
    position: absolute;
    top:60px;
    right:50px;
    z-index: 999;
    cursor:pointer;
    background: #fff;
    border-radius: 5px;
    /*opacity: 0.9;*/
  }
  .systenHover li{
    font-size: 14px;
    color: black;
    display: block;
    height:30px;
    line-height: 30px;
    padding:0 40px;
    text-align: center;
  }
  .systenHover li:hover{
    background: #9e9ea6;
  }

 
</style>

<style>
 .headers .ivu-dropdown-rel{
    display: flex !important;
    align-items: center !important;
  }
</style>

