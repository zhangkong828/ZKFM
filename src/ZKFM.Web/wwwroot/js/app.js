requirejs.config({
    baseUrl: 'js/lib',
    paths: {
        jquery: '../jquery.min.js',
        lib: '../lib'
    }
});

requirejs(['jquery'], function ($) {

});