//é só popular o valor em uma div - $(var_relogio).text(getFormattedDate(str));
$(document).ready(function () {

    var var_relogio = $('#var_relogio');

    let ano, mes, dia, hora, minuto, segundo;

    var str;



    setInterval(function () {

        $(var_relogio).text(getFormattedDate(str));

    }, 1000);



    function getFormattedDate(ano, mes, dia, hora, minuto, segundo) {

        // Não funciona - dia < 9 ? "0" + (dia) : dia;

        var date = new Date();



        dia = date.getDate();

        dia = (dia < 10 ? "0" : "") + dia;



        mes = (date.getMonth() + 1);

        mes = (mes < 10 ? "0" : "") + mes;



        ano = date.getFullYear();



        hora = date.getHours();

        hora = (hora < 10 ? "0" : "") + hora;



        minuto = date.getMinutes();

        minuto = (minuto < 10 ? "0" : "") + minuto;



        //return str;

        return str = dia + "/" + mes + "/" + (ano - 2000) + " " + hora + ":" + minuto;

    }

});