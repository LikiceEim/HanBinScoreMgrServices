<template>
    <LeaderList :td="leaderli" ></LeaderList>
  <!--@selectBtn="dataBtn" :td="leaderli"-->
</template>

<script>
  import LeaderList from "../components/leaderList/leaderList"
    export default {
        name: "leaderList",
      components :{LeaderList},
      data(){
        return {
           leaderli:[]
        }
      },
      created(){
         this.$axios({
           url:'../../static/json/leader.json',
           method:'get'
         }) .then((res)=>{
           // console.log(res)
          //  this.leaderli=res.data;
          // this.leaderli=[];
         }) .catch((error)=>{
           // console.log(error)
         })
      },
      // methods:{
      //   dataBtn(val){
      //     this.$axios({
      //       url:'url',
      //       method:"post",
      //       // data:{CjList:this.list}
      //       data:this.list
      //     }).then((res)=>{
      //       this.list=res.data
      //       console.log(res)
      //     })
      //   }
      // }
    }
</script>

<style scoped>

</style>
