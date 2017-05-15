requirejs.config({
    baseUrl: 'js/lib',
    paths: {
        jquery: '../jquery.min',
        lib: '../lib'
    }
});

requirejs(['jquery', 'lib/config', 'lib/logger', 'lib/player', 'lib/controller'], function ($, config, logger, player, controller) {
    player.start(1);
});