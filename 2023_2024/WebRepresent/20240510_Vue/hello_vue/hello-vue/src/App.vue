<template>
  <!--View-->
  <h1>Hello, Vue project</h1>
  Введите ваше имя:
  <input type="text" size="10" v-model="user"/>
  <button type="button" @click="greetUser()">Ввод</button>
  {{ greeting }}
</template>

<script>
  /*Model, Controller*/
  export default {
    data(){
      /*Model*/
      return {
        user: "",
        greeting: ""
      }
    },
    "methods":{
      greetUser(){
        this.greeting="Здравствуйте, "+ this.user +" !";
      }
    }
  }
</script>

<style scoped>
h1 {
  color: red;
}
</style>
