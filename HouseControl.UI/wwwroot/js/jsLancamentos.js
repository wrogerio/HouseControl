$(document).ready(function () {

    if (window.location.pathname == "/Lancamentos") {
        loadCategorias()
    }

    // key upper to change . to ,
    $("#Valor").keyup(function () {
        var valor = $(this).val();
        console.log(valor);
        valor = valor.replace(".", ",");
        $(this).val(valor);
    });
});


function loadCategorias() {
    const grid = new gridjs.Grid({
        columns: [
            { name: 'Data', width: "30px" },
            { name: 'Ano', width: "30px" },
            { name: 'Mes', width: "30px" },
            { name: 'Quinzena', width: "30px" },
            { name: 'Categoria'  },
            { name: 'Valor', width: "50px" },
            { name: 'Parcela ', width: "30px" },
            { name: 'Parcelas', width: "30px" },
            {
                name: 'Ação',
                width: "30px",
                formatter: function (id) {
                    return gridjs.html(
                        "<a href='/Lancamentos/Alterar/" + id + "'>" +
                        "<i class='fad fa-edit me-2'></i>" +
                        "</a>" +
                        "<a href='/Lancamentos/Remover/" + id + "'>" +
                        "<i class='fad fa-trash'></i>" +
                        "</a>"
                    )
                }
            }],
        server: {
            url: '/Lancamentos/GetLancamentos',
            then: data => {
                return data.map(item => {
                    return [
                        item.dtLancamento,
                        item.ano,
                        item.mesNome,
                        item.quinzena,
                        item.categoria,
                        item.valor,
                        item.parcela,
                        item.totalParcelas,
                        item.id
                    ]
                })
            }
        },
        sort: !0,
        search: !0,
        pagination: { limit: 20 },
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
    }).render(document.getElementById("tableLancamentos"))
}