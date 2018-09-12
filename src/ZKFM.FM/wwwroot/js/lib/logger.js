define(['lib/config'], function (config) {
    return {
        debug: function (msg) {
            if (config.default.logger.debug)
                console.log(msg);
        },
        info: function (msg) {
            if (config.default.logger.info)
                console.info(msg);
        },
        warn: function (msg) {
            console.warn(msg);
        },
        error: function (msg) {
            console.error(msg);
        }
    }
});