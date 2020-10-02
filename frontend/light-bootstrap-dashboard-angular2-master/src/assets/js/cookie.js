function deleteCookie(name) {

    if (getCookie(name)) {

        document.cookie =
            name + "=" +

            "; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;";

        //history.go(0);

    }

}



function
    getCookie(name) {

    var dc =
        document.cookie;

    var prefix =
        name + "=";

    var begin =
        dc.indexOf("; " +
            prefix);

    if (begin == -1) {

        begin = dc.indexOf(prefix);

        if (begin !=
            0) return
        null;

    } else

        begin +=
            2;

    var end =
        document.cookie.indexOf(";",
            begin);

    if (end == -1)

        end = dc.length;

    return unescape(dc.substring(begin +
        prefix.length,
        end));

}



function
    createCookie(name,
        value, expires,
        path, domain) {

    var cookie =
        name + "=" +
        escape(value) +
        ";";



    if (expires) {

        // If it's a date

        if (expires
            instanceof Date) {

            // If it isn't a valid date

            if (isNaN(expires.getTime()))

                expires =
                    new Date();

        }

        else

            expires =
                new Date(new
                    Date().getTime() +
                    parseInt(expires) *
                    1000 * 60 *
                    60 * 24);



        cookie +=
            "expires=" + expires.toGMTString() +
            ";";

    }



    if (path)

        cookie +=
            "path=" + path +
            ";";

    if (domain)

        cookie +=
            "domain=" + domain +
            ";";



    document.cookie =
        cookie;

}



//Exemplo de criação de cookie//

createCookie("username",
    "Caue kkk");





//Exemplo de checar se cookie existe//

if (getCookie("username")) {

    username = getCookie("username");

};





//Exemplo de remoção de cookie//

deleteCookie("username");
