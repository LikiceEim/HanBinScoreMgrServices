<template>
  <div id="app">
    <!--<Login></Login>-->
    <router-view/>
  </div>
</template>

<script>

  export default {
    name: 'App',

    created(){
      // this.$router.push("/")
    }

}
</script>

<style>
  #app{
    width: 100%;
    height: 100%;
    /*overflow: auto;*/
  }
</style>
