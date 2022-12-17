/// <reference path="typings/jquery/jquery.d.ts" />
$(function () {
    const loginBox = $("#Login");
    const passwordBox = $("#Password");
    const passwordCheckBox = $("#PasswordCheck");
    const firstNameBox = $("#FirstName");
    const secondNameBox = $("#Surname");
    const fatherNameBox = $("#FatherName");
    const phoneBox = $("#MobilePhone");
    const emailBox = $("#Email");
    const submitBtn = $("#Submit");
    const allInputBoxes = $("input");
    allInputBoxes.each(() => {
        $(this).hover(() => {
            if (IsAllInputsValid())
                revealButton(submitBtn);
        }, () => {
            if (IsAllInputsValid())
                revealButton(submitBtn);
        });
    });
    function IsAllInputsValid() {
        if (!isValidLogin(loginBox.val())) {
            return false;
        }
        if (!isValidPassword(passwordBox.val())) {
            return false;
        }
        if (!isPasswordAndCheckSame((passwordBox.val()), passwordCheckBox.val())) {
            return false;
        }
        //Проверка ФИО
        if (!isValidName(firstNameBox.val()) && !isValidName(secondNameBox.val()) && !isValidName(fatherNameBox.val())) {
            return false;
        }
        if (!isValidEmail(emailBox.val())) {
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
    function isValidEmail(email) {
        if (!email.includes('@')) {
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
//# sourceMappingURL=client_reg.js.map