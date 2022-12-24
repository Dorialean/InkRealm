$(function () {
    let deleteButtons = $("button[id]");
    deleteButtons.each((index, btn) => {
        $(btn).click(() => {
            let prodId = $(btn).attr('id').slice($(btn).attr('id').lastIndexOf("_") + 1);
            let pk = $(btn).attr('id').split('_');
            console.log(pk);
            if (pk) {
                fetch('/api/InkClientServices/' + pk[0] + '/' + pk[1] + '/' + pk[2] + '/true', {
                    method: 'PUT',
                })
                .catch(err => alert(err));
            }
        });
    });
});