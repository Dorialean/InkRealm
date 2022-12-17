/// <reference path="typings/jquery/jquery.d.ts" />
$(function(){
    let realPass = $('#Password');
    let samePass = $('#same_pass');
    
    realPass.change(() => {
        console.log($(this).val().length);
        if($(this).val().length <= 8){
            $(this).append('<span class="alert alert-danger">Слишком короткий пароль</span>');
        }
    });
});