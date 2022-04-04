import LazySlideCaptcha from './index.vue';

/* istanbul ignore next */
LazySlideCaptcha.install = function(Vue) {
  Vue.component(LazySlideCaptcha.name, LazySlideCaptcha);
};

export default LazySlideCaptcha;