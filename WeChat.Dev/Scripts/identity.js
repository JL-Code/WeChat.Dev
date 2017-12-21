(function (host, factory) {

    'use strict';
    host.IdentityHandler = factory();

}(this, function () {

    'use strict';

    var _key = "_identity_";
    var _tokenPath = "/api/token";

    function IdentityHandler(appcode, website) {
        this.appcode = appcode;
        this.website = website;
        _key = this.appcode + _key;
        _tokenPath = this.website + _tokenPath;
    }

    //授权
    IdentityHandler.prototype.authorize = function () {

    }

    //认证
    IdentityHandler.prototype.authenticate = function () {
        if (!verifyToken(_key)) {
            window.location.href = _tokenPath;
        }
    }

    function verifyToken(key) {
        var flag = false;
        var token = this.getToken(key);
        if (token && token.expires_in) {
            flag = true;
        }
        return flag;
    }

    function saveToken(key, token) {
        var tokenStr = token;
        if (typeof token === 'object') {
            tokenStr = JSON.stringify(token);
        }
        localStorage.setItem(key, tokenStr);
    }

    function getToken(key) {
        try {
            var tokenStr = localStorage.getItem(key);
            return JSON.parse(tokenStr);
        } catch (e) {
            return {};
        }
    }

    function removeToken(key) {
        localStorage.removeItem(key);
    }

}));