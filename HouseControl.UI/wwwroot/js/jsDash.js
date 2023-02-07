﻿$(document).ready(function () {
    loadData()

    $("#MesAno").change(function () {
        loadData();
    });
});


function loadData() {
    var mesAno = $("#MesAno").val();
    $.ajax({
        url: "home/GetResumoMensal?MesAno=" + mesAno,
        type: "GET",
        dataType: "json",
        success: function (data) {
            var dataList = data.dados;

            // order by porcentagem desc
            _dataList = dataList.sort(function (a, b) {
                return b.porcentagem - a.porcentagem;
            });

            $("#rowDash").empty();
            $("#gastoTotal").text(data.total.toLocaleString('pt-br', { style: 'currency', currency: 'BRL' }))

            var _html = _dataList.map(x => {
                return `
                    <div class="col-2">
                        <div class="card">
                            <div class="card-header p-2 m-0 text-center">
                                <h4 class="m-0">${x.categoria}</h4>
                            </div>
                            <div class="card-body p-4 bg-secondary text-white text-center">
                                <h3>${x.valor.toLocaleString('pt-br', { style: 'currency', currency: 'BRL' })}</h3>
                                <h5>${x.porcentagem.toFixed(2)} %</h5>
                            </div>
                        </div>
                    </div>`
            })
            
            $("#rowDash").append(_html);
        },
        error: function (errormessage) {
            console.log(errormessage);
        }
    })
}