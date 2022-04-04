import LazySlideCaptcha from './lazy-slide-captcha/index';

const components = [
  LazySlideCaptcha
];

const install = function(Vue) {
  components.forEach(component => {
    Vue.component(component.name, component);
  });
};

/* istanbul ignore if */
if (typeof window !== 'undefined' && window.Vue) {
  install(window.Vue);
}

export default {
  install
};