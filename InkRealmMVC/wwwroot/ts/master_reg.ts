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

    function isAllInputsValid(): boolean {
        return true;
    }

    
    //Сюда надо импортировать методы из client_reg
    function revealButton(sumbit) {
        if (sumbit.hasClass("disabled")) {
            sumbit.removeClass("disabled");
        }
    }


});