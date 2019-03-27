<template>
    <UnitList :tn="dataList" ></UnitList>
  <!--<&#45;&#45;!@btn6="btn8"&ndash;&gt;-->
</template>

<script>
  import UnitList from "../components/unitList/unitList"
    export default {
      name: "unitList",
      components:{UnitList},
      data(){
        return {
          dataList:[]
        }
      },

      // methods:{
      //   btn8(val){
      //     console.log(val)
      //     // this.$axios({
      //     //   url:'url',
      //     //   method:'post',
      //     //   data:{selectData:val}
      //     // }).then((res)=>{
      //    
      //     // })
      //   }
      // },
      created(){
          this.$axios({
            url:'../../static/json/tableData.json',
            method:'get'
          }).then((res)=>{
            debugger;
            // console.log(res);
            // this.dataList=res.data
            this.dataList = [];
          })
      }
    }
</script>

<style scoped>

</style>
