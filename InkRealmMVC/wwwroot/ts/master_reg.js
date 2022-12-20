/// <reference path="typings/jquery/jquery.d.ts" />
$(function () {
    const loginBox = $("#Login");
    const passwordBox = $("#Password");
    const passwordCheck = $("#PasswordCheck");
    const firstNameBox = $("#FirstName");
    const secondNameBox = $("#SecondName");
    const submitBtn = $("#Submit");
    $("input").each(() => {
        $(this).hover(() => {
            if (isAllInputsValid()) {
                revealButton(submitBtn);
            }
        }, () => {
            if (isAllInputsValid()) {
                revealButton(submitBtn);
            }
        });
    });
    function isAllInputsValid() {
        if (!isValidLogin(loginBox.val())) {
            return false;
        }
        if (!isValidPassword(passwordBox.val())) {
            return false;
        }
        if (isPasswordAndCheckSame(passwordBox.val(), passwordCheck.val())) {
            return false;
        }
        if (!isValidName(firstNameBox.val())) {
            return false;
        }
        if (!isValidName(secondNameBox.val())) {
            return false;
        }
        return true;
    }
    function isValidLogin(login) {
        if (login.length <= 2 || login.includes('-')) {
            return false;
        }
        return true;
    }
    function isValidPassword(password) {
        if (password.length < 8) {
            return false;
        }
        if (!/\d/.test(password)) {
            return false;
        }
        if (!/[a-zA-Z]/.test(password)) {
            return false;
        }
        return true;
    }
    function isPasswordAndCheckSame(realPassword, repeatPassword) {
        if (realPassword !== repeatPassword) {
            return false;
        }
        return true;
    }
    function isValidName(name) {
        if (name.length <= 1 || !/^[a-zA-Z]+$/.test(name)) {
            return false;
        }
        return true;
    }
    function revealButton(sumbit) {
        if (sumbit.hasClass("disabled")) {
            sumbit.removeClass("disabled");
        }
    }
});
//# sourceMappingURL=master_reg.js.map