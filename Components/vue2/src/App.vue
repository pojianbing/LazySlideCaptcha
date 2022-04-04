<template>
  <div id="app">
    <div class="container">
      <SlideCaptcha 
        :width="300" 
        :height="183" 
        :showRefresh="true"
        @finish="handleFinish" 
        ref="slideCaptcha" 
      />
    </div>
  </div>
</template>

<script>
import SlideCaptcha from './package/lazy-slide-captcha/index'
import img1 from './assets/1.png'
import img2 from './assets/2.png'

export default {
  name: 'App',
  components: {
    SlideCaptcha
  },
  data(){
    return {

    }
  },
  mounted(){
    this.generate()
  },
  methods: {
    handleFinish: function() {
      this.$refs.slideCaptcha.startRequestVerify()

      setTimeout(() => {
        let isPassing = Math.random() > 0.5;
        this.$refs.slideCaptcha.endRequestVerify(isPassing)
      }, 500);
    },
    generate(){
      this.$refs.slideCaptcha.startRequestGenerate()
      
      setTimeout(() => {
        let success = Math.random() > 0.1
        if(success){
          this.$refs.slideCaptcha.endRequestGenerate(img1, img2)
        } else {
          this.$refs.slideCaptcha.endRequestGenerate(null, null)
        }
      }, 500);
    }
  }
}
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
  display: flex;
  align-items: center;
  justify-content: center;
}

.container{
  padding: 10px 5px;
  background-color: #fff;
  z-index: 999;
  box-sizing: border-box;
  padding: 9px;
  border-radius: 6px;
  box-shadow: 0 0 2px 0px #858383;
}

</style>
