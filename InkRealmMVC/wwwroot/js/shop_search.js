$(function () {

    let searchBtn = $("#SearchBtn");
    let searchBox = $("#SearchBox");
    let searchRes = $("#SearchRes");

    searchBtn.click(() => {
        const searchInfo = searchBox.val();
        if (isSearchValid(searchInfo)) {
            let filteredProducts = filterProducts(allProducts, searchInfo);
            searchRes.html("");
            filteredProducts.forEach((product) => {
                searchRes.append('<div id="SearchRes" class="col-3 card me-3 mt-3 mb-2">');
                if (product.photoLink) {
                    searchRes.append('<img class="card-img-top" src=@p.PhotoLink style="max-height:200px;max-width:200px;"/>');
                }
                searchRes.append('<div class="card-body">');
                searchRes.append('<p class="card-title fw-bold h3">' + product.title + '</p>')
                if (product.description) {
                    searchRes.append('<p class="card-text p-3">' + product.description + '</p>');
                }
                searchRes.append('<p class="card-text">Цена: <strong class="text-success">' + product.eachPrice + ' ₽</strong></p>');
                if (isUserInClientRole) {
                    let prodId = "product_" + product.productId;
                    searchRes.append('<button class="btn btn-outline-success" id=' + prodId + '>Купить</button>');
                }
                searchRes.append('</div>');
                searchRes.append('</div>');
            });
        }
    });

    function isSearchValid(searchQuery) {
        if (!searchQuery) {
            return false
        }
        return true;
    }

    function filterProducts(prods, search) {
        res = new Set();
        for (prod of prods) {
            if (prod.title.toLowerCase().includes(search.toLowerCase()) || prod.description.toLowerCase().includes(search.toLowerCase())) {
                res.add(prod);
            }
        }
        return res;
    }
});