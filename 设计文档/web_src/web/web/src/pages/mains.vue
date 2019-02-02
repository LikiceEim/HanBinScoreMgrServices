<template>
    <div class="mainCenter">
      <div class="headers">
        <Headers ></Headers>
      </div>
      <div class="center">
        <Row>
          <i-col span="3" class="lef">  <Navs></Navs></i-col>
          <i-col span="21" class="rig" >
            <div class="bg-white">
              <router-view/>

            </div>

          </i-col>
        </Row>
      </div>
    </div>
</template>


<script>
  import Headers from "../components/headers";
  import Navs from "../components/navs";
    export default {
        name: "mains",
      components:{Headers,Navs},
    }
</script>

<style scoped>
  .mainCenter{
    height:100%;
  }
  .headers{
    height: 7%;
  }
  .ivu-row{
    height:100%;
  }
  .center{
    width: 100%;
    height: 93%;
    background: #eff0f6;
  }
  .lef{
    height: 100%;
    /*margin-right: 15px;*/
    min-width: 190px !important;
  }
  .rig{
    height: 100%;
    background: #f5f7f9;
    /*margin-top: 20px;*/
    overflow: auto;
    max-width: calc(100% - 190px) !important;
  }
  .bg-white{
    background: #fff;
    margin: 20px 15px;
  }
  /* .ivu-col-span-3 {
    min-width: 190px !important;
  } */
</style>
