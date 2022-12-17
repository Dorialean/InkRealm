/// <reference path="typings/jquery/jquery.d.ts" />
$(function () {
    let passwordBox = $('#password');
    passwordBox.on('input', () => {
        let loginBox = $('#login');
        if (loginBox.val()) {
            let submitBtn = $('#submit_auth');
            submitBtn.removeClass('disabled');
        }
    });
});
//# sourceMappingURL=auth.js.map