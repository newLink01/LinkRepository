function GoToVK() {

    window.location.href = 'https://oauth.vk.com/authorize?client_id=5425400&redirect_uri=http://localhost:63643/Home/Index&display=page&v=5.50&response_type=token';

    $(window).on('popstate', function (e) {

        Application.updateState();
        SetToken();
    });
}

function SetToken() {
    var accessToken = "";
    var currentUrl = window.location.href.toString();
    var currentInd = currentUrl.indexOf('access_token=');


    if (currentInd == -1) { return 'WRONG'; }
    currentInd += 'access_token='.length;

    //alert(currentUrl.substr(currentInd,currentInd+10));
        
    for (currentInd; currentInd < currentUrl.length; currentInd++) {
        if (currentUrl[currentInd] == '&') { break; }
        else {
            accessToken += currentUrl[currentInd];
        }
    }

    $.get("../Home/SetToken", {accessToken : accessToken} , function (data) {
        alert("Token has been received.");
    });
}