$(function () {
    let buyButtons = $("button[id^='product']");
    buyButtons.each((index, btn) => {
        $(btn).click(() => {
            let prodId = $(btn).attr('id').slice($(btn).attr('id').lastIndexOf("_") + 1);
            const data = {
                ProductId: prodId,
                ClientId: clientId
            };

            if (clientId) {
                fetch('/api/Orders', {
                    method: 'POST',
                    headers: {
                        'Accept': 'application/json',
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify(data)
                })
                    .catch(err => alert(err));
            }
            else {
                alert("Вы не зарегистированы.");
            }
        });
    }); 
});