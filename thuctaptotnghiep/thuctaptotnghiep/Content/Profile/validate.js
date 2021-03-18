var vnw_validate = {
    check: function (obj, err_id, func) {
        var errObj = null;
        if (err_id == null) {
            errObj = jQuery('#err_' + obj.name);
        } else if (typeof err_id == 'string') {
            errObj = jQuery('#' + err_id);
        } else {
            errObj = err_id;
        }
        errObj.css('display', 'none');
        if (!this[func](obj)) {
            errObj.css('display', 'block');
            return false;
        }
        return true;
    }, ltrim: function (_1) {
        var _2 = /^\s */;
        return _1.replace(_2, "");
    }, rtrim: function (_3) {
        var _4 = /\s *$/;
        return _3.replace(_4, "");
    }, trim: function (_5) {
        return this.ltrim(this.rtrim(_5));
    }, _checkRequired: function (obj) {
        var st = this.trim(obj.value);
        return !(st === '' || st === $(obj).attr('placeholder'));
    }, checkRequired: function (obj, err_id) {
        return this.check(obj, err_id, '_checkRequired');
    }, _checkEmail: function (obj) {
        var st = this.trim(obj.value), _46 = "^[_a-zA-Z0-9-]+(\\.[_a-zA-Z0-9-]+)*@[a-zA-Z0-9-]+(\\.[a-zA-Z0-9-]+)*(\\.[a-zA-Z]{2,8})$", _47 = new RegExp(_46);
        return _47.exec(st);
    }, checkEmail: function (obj, err_id) {
        return this.check(obj, err_id, '_checkEmail');
    }, _checkPhone: function (obj) {
        var _40 = "0123456789-+() ", _41 = this.trim(obj.value);
        if (_41.length < 1) {
            return false;
        }
        for (i = 0; i < _41.length; i++) {
            ch = _41.charAt(i);
            if (_40.indexOf(ch) < 0) {
                return false;
            }
        }
        return true;
    }, checkPhone: function (obj, err_id) {
        return this.check(obj, err_id, '_checkPhone');
    }, _checkInt: function (obj) {
        var _2a = this.trim(obj.value);
        if (_2a.length < 1 || _2a == "") {
            return false;
        }
        var _2b = "0123456789";
        for (i = 0; i < _2a.length; i++) {
            ch = _2a.charAt(i);
            if (_2b.indexOf(ch) < 0) {
                return false;
            }
        }
        if (_2a.valueOf() <= 0) {
            return false;
        }
        return true;
    }, _checkTax: function (obj) {
        var _2a = this.trim(obj.value);
        if (_2a.length < 1 || _2a == "") {
            return false;
        }
        var _2b = "0123456789-_";
        for (i = 0; i < _2a.length; i++) {
            ch = _2a.charAt(i);
            if (_2b.indexOf(ch) < 0) {
                return false;
            }
        }
        if (_2a.valueOf() <= 0) {
            return false;
        }
        return true;
    }, checkInt: function (obj, err_id) {
        return this.check(obj, err_id, '_checkInt');
    }
}