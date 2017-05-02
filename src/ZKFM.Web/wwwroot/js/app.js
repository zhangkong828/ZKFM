requirejs.config({
    baseUrl: 'js/lib',
    paths: {
        jquery: '../jquery.min',
        lib: '../lib'
    }
});

requirejs(['jquery','lib/logger'], function ($,logger) {
   
});