<template>
   <div>
     <IntQuery :int="intqu"></IntQuery>
   </div>
</template>

<script>
  import IntQuery from "../components/intQuery/intQuery"
    export default {
        name: "intQuery",
        components:{IntQuery},
      data(){
          return{
            intqu:[],
          }
      } ,
      created(){
        // this.$axios({
        //   url:'../../static/json/leader.json',
        //   method:'get'
        // }) .then((res)=>{
        //   console.log(res)
        //   this.leaderli=res.data;
        // }) .catch((error)=>{
        //   console.log(error)
        // })
        this.$axios({
          url:'../../static/json/intqu.json',
          method:'get',
        }).then((res)=>{
          console.log(res)
          // this.intqu=res.data;
        })
      }
    }
</script>

<style scoped>

</style>
