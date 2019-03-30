<template>
  <LeaderGraphic></LeaderGraphic>
</template>


<script>
import LeaderGraphic from "../components/leaderList/leaderGraphic"

export default {
     name: "leaderGraphic",
      components :{LeaderGraphic},
      data(){
        return {
          
        }
      },
}
</script>

<style>
.list-red {
  width: 100%;
  justify-content: space-between;
  height: 300px;
  background-color: #fff;
}
</style>

