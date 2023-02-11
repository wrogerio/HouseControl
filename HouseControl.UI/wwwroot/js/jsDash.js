$(document).ready(function () {
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
            var mesAno = document.getElementById("MesAno").value;
            var mesNome = traduzMesNome(mesAno.split("-")[1]).toUpperCase();
            var ano = mesAno.split("-")[0];

            // order by porcentagem desc
            _dataList = dataList.sort(function (a, b) {
                return b.porcentagem - a.porcentagem;
            });

            $("#rowDash").empty();
            $("#gastoTotal").text(mesNome + "/" + ano + " " + data.total.toLocaleString('pt-br', { style: 'currency', currency: 'BRL' }))

            var _html = _dataList.map(x => {
                return `
                    <div class="col-12 col-sm-6 col-md-4 col-lg-3 col-xl-2">
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

function traduzMesNome(mes) {
    var nomeMes = ""
    switch (mes) {
        case '01':
            nomeMes = "Janeiro";
            break;
        case '02':
            nomeMes = "Fevereiro";
            break;
        case '03':
            nomeMes = "Março";
            break;
        case '04':
            nomeMes = "Abril";
            break;
        case '05':
            nomeMes = "Maio";
            break;
        case '06':
            nomeMes = "Junho";
            break;
        case '07':
            nomeMes = "Julho";
            break;
        case '08':
            nomeMes = "Agosto";
            break;
        case '09':
            nomeMes = "Setembro";
            break;
        case '10':
            nomeMes = "Outubro";
            break;
        case '11':
            nomeMes = "Novembro";
            break;
        case '12':
            nomeMes = "Dezembro";
            break;
        default:
            nomeMes = "Janeiro";
            break;
    }

    return nomeMes;
}