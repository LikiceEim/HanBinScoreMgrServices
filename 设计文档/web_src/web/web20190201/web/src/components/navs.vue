<template>
    <div class="iv-nav">

          <Menu class="navs" :open-names="['1']" accordion :theme="theme" width="auto" ><!--:theme="dark"-->
            <Submenu name="1" >
              <template slot="title" >
                <!--to="/HomePages"-->
                <Icon type="ios-paper" />
                工作台
              </template>
              <MenuItem name="1-1" to="/Mains/HomePages">系统工作台</MenuItem>
            </Submenu>

            <Submenu v-if="roleID==1||roleID==3" name="2">
              <template slot="title">
                <Icon type="ios-people" />
                单位管理
              </template>
              <!--<MenuItem name="2-1"  to="/Mains/CreateUnit" repalce="true">创建单位</MenuItem>-->
              <MenuItem name="2-2" to="/Mains/UnitList">单位列表</MenuItem>
            </Submenu>

            <Submenu v-if="roleID==1||roleID==4" name="3">
              <template slot="title">
                <Icon type="ios-stats" />
                干部管理
              </template>
              <!--<MenuItem name="3-1" to="/Mains/CreateLeader" >创建干部</MenuItem>-->
              <MenuItem name="3-2" to="/Mains/LeaderList">干部列表</MenuItem>
              <MenuItem name="3-4" to="/Mains/LeaderGraphic" v-if="roleID==4">图形显示</MenuItem>
            </Submenu>

            <Submenu name="4">
              <template slot="title">
                <Icon type="ios-pie" />
                积分查询系统
              </template>
              <MenuItem to="/Mains/IntQuery" name="4-1">积分查询</MenuItem>
              <MenuItem to="/Mains/dimensionalPre" v-if="roleID==1||roleID==3" name="4-2">分维度展示</MenuItem>
            </Submenu>

            <Submenu name="5">
              <template slot="title">
                <Icon type="ios-cog" />
                系统管理
              </template>
              <MenuItem to="/Mains/UsersManage" v-if="roleID==1||roleID==3" name="5-1">用户管理</MenuItem>
              <MenuItem to="/Mains/DicManaged" name="5-2" v-if="roleID==1||roleID==3">字典管理</MenuItem>
              <MenuItem to="/Mains/LogManage" name="5-3">日志管理</MenuItem>
              <MenuItem to="/Mains/Backups" name="5-4" v-if="roleID==1||roleID==3">备份还原</MenuItem>
            </Submenu>
          </Menu>
    </div>
</template>

<script>
import Cookies from 'js-cookie'
    export default {
        name: "navs",
      data(){
          return{
            // 权限控制
            roleID:null,
            theme:'dark',

          }
      },
      created(){
        this.roleID = Cookies.get('RoleID');
      }
      // methods:{
      //   go(){
      //     this.$router.push("/HomePages")
      //   }
      // }
    }

</script>

<style scoped>
.iv-nav{
  width: 100%;
  height: 100%;
  background: #515a6e;
}
  .navs{
    padding-top: 40px;
  }
</style>
<style>

</style>
