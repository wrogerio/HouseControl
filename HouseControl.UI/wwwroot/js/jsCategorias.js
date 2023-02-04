$(document).ready(function () {
    loadCategorias()
});


function loadCategorias() {
    const grid = new gridjs.Grid({
        columns: [
            {
                name: 'Categoria'
            },
            {
                name: 'Ação',
                width: "80px",
                formatter: function (id) {
                    return gridjs.html(
                        "<a href='/Categorias/Alterar/" + id + "'>" +
                        "<i class='fad fa-edit me-2'></i>" +
                        "</a>" +
                        "<a href='/Categorias/Remover/" + id + "'>" +
                        "<i class='fad fa-trash'></i>" +
                        "</a>"
                    )
                }
            }],
        server: {
            url: '/Categorias/GetCategorias',
            then: data => {
                return data.map(item => {
                    return [item.nome, item.id]
                })
            }
        },
        sort: !0,
        search: !0,
        pagination: { limit: 10 },
        language: {
            'search': {
                'placeholder': '🔍 Procurar...'
            },
            'pagination': {
                'previous': '⬅️',
                'next': '➡️',
                'showing': '😃 Exibindo',
                'results': () => 'Registros'
            }
        }
    }).render(document.getElementById("tableCategorias"))
}