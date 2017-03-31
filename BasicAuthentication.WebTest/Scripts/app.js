var token;
var refreshToken;
var timer;

$(document).ready(function ()
{
    checkIfLoggedIn();
});

function checkIfLoggedIn()
{
    $.get("api/v1/initializeSystem", function ()
    {
        $('#loginDiv').css('display', 'none');
        $('#contentDiv').css('display', 'block');
    })
    .fail(function (a, b, c)
    {
        if (a.status == 401)
        {
            $('#loginDiv').css('display', 'block');
            $('#contentDiv').css('display', 'none');
        }
        else
        {
            alert('error');
        }
    });
}


function login()
{
    var data = "grant_type=password&username=" + 'Steve' + "&password=" + 'password' + "&client_id=" + 'myClientId';
    $.post('http://localhost/BasicAuthTest/api/v1/token', data, function (a,b,c)
    {
        //console.log(a);
        console.log(a.access_token);
        console.log(a.refresh_token);

        token = a.access_token;
        refreshToken = a.refresh_token;

        startTimer();

        $('#loginDiv').css('display', 'none');
        $('#contentDiv').css('display', 'block');
    })
    .fail(function (a, b, c)
    {
        $('#loginDiv').css('display', 'block');
        $('#contentDiv').css('display', 'none');
        alert('login error');
    });
}

function startTimer()
{
    if (timer == null)
    {
        timer = setInterval(pingTest, 10000);
    }
}

function pingTest()
{
    $.ajax(
        {
            url: "api/v1/pingTest",
            beforeSend: function (xhr)
            {
                xhr.setRequestHeader("Authorization", "Bearer " + token);
            },
        }).done(function()
        {
        console.log('ping successfull');
    })
    .fail(function (a, b, c)
    {
        clearInterval(timer);
        console.log('ping error');
        console.log(a);
        console.log(b);
        console.log(c);
        tryRefreshToken();
    });
}

function tryRefreshToken()
{
    console.log('trying refresh token');
    //var data = "grant_type=refresh_token&refresh_token=" + refreshToken + "&client_id=" + 'myClientId';
    var data =
        {
            "grant_type": "refresh_token",
            "refresh_token": refreshToken,
            //"refresh_token": token,
            "client_id": "myClientId"
        };
    
    $.ajax(
        {
            url: "http://localhost/BasicAuthTest/api/v1/token",
            method: "POST",
            headers: {
                "Authorization": token
            },
            data: data
        }).done(function (resp)
        {
            console.log('refresh token successfull');
            refreshToken = resp.refresh_token;
            token = resp.access_token;
        })
    .fail(function (a, b, c)
    {
        console.log('refresh error');
        console.log(a);
        console.log(b);
        console.log(c);
    });
}
