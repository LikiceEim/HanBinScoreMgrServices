<template>
    <UsersManage :users="users"></UsersManage>
</template>

<script>
  import UsersManage from "../components/usersManage/usersManage"
    export default {
      name: "usersManage",
      components:{UsersManage},
      data(){
          return {
            users:[],
          }
      },
      created(){
        this.$axios({
          url:'../../static/json/usersjson.json',
          method:'get',
        }).then((res)=>{
          // console.log(res)
          // this.users=res.data;
          this.users=[];
        })
      }
    }
</script>

<style scoped>
  .ivu-table td, .ivu-table th{
    height:5vh;
    padding:2vh;
  }
  .ivu-table-cell{
    white-space: nowrap;
  }
</style>
