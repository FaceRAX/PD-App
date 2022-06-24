let formIteration = 0;

function NextForm(s, e) {
    
    let loopEnd = false;
    var form0Iterations = 0;
    while (!loopEnd) {
        var myEle = document.getElementById("content" + form0Iterations);
        if (myEle) {
            form0Iterations++;
        }
        else
        {
            loopEnd = true;
            form0Iterations--;
        }
    }
    if (document.getElementById("next_txt").innerHTML == "Validar e Finalizar") {
        FinishAttempt();
    }
    if (formIteration >= form0Iterations) { return; }
    const form0Step = 100 / form0Iterations;
    console.log('Loading Layer ' + formIteration);
    let formlayer = "content";
    let layerUnderline = "page"
    let currentlayer = layerUnderline + formIteration;
    let currentForm = formlayer + formIteration;
    document.getElementById(currentForm).style.display = "none";
    document.getElementById(currentlayer).style.borderBottom = "";
    //document.getElementById("perc_label").innerHTML = (formStep * formIteration) + "%";
    SubmitForm0(s, e);
    formIteration = formIteration + 1;
    let progress = form0Step * formIteration;
    progress = progress.toFixed(1);
    let progress_str = progress + "%";
    document.getElementById("perc_label_n").innerHTML = progress_str;
    $('#perc_graph').attr("aria-valuenow", progress);
    $('#perc_graph').attr("style", "width:" + progress_str);
    if (formIteration > 0) {
        document.getElementById("back").style.display = "inline-flex";
    }
    else {
        document.getElementById("back").style.display = "none";
    }
    if (formIteration == form0Iterations) {
        document.getElementById("next_txt").innerHTML = "Validar e Finalizar";
    }
    else {
        document.getElementById("next_txt").innerHTML = "Guardar e Avançar";
    }
    let nextForm = formlayer + formIteration;
    document.getElementById(nextForm).style.display = "block";
    let nextLayerLine = layerUnderline + formIteration;
    document.getElementById(nextLayerLine).style.borderBottom = "2px solid currentColor";
}

function PreviousForm(s, e) {

    let loopEnd = false;
    var form0Iterations = 0;
    while (!loopEnd) {
        var myEle = document.getElementById("content" + form0Iterations);
        if (myEle) {
            form0Iterations++;
        }
        else {
            loopEnd = true;
            form0Iterations--;
        }
    }
    if (formIteration <= 0) { return;}
    const form0Step = 100 / form0Iterations;
    console.log('Loading Layer ' + formIteration);
    let formlayer = "content";
    let layerUnderline = "page"
    let currentlayer = layerUnderline + formIteration;
    let currentForm = formlayer + formIteration;
    document.getElementById(currentForm).style.display = "none";
    document.getElementById(currentlayer).style.borderBottom = "";
    //document.getElementById("perc_label").innerHTML = (formStep * formIteration) + "%";
    formIteration = formIteration - 1;
    let progress = form0Step * formIteration;
    progress = progress.toFixed(1);
    let progress_str = progress + "%";
    document.getElementById("perc_label_n").innerHTML = progress_str;
    $('#perc_graph').attr("aria-valuenow", progress);
    $('#perc_graph').attr("style", "width:" + progress_str);
    if (formIteration > 0) {
        document.getElementById("back").style.display = "inline-flex";
    }
    else {
        document.getElementById("back").style.display = "none";
    }
    if (formIteration == form0Iterations) {
        document.getElementById("next_txt").innerHTML = "Validar e Finalizar";

    }
    else {
        document.getElementById("next_txt").innerHTML = "Guardar e Avançar";
    }
    let nextForm = formlayer + formIteration;
    document.getElementById(nextForm).style.display = "block";
    let nextLayerLine = layerUnderline + formIteration;
    document.getElementById(nextLayerLine).style.borderBottom = "2px solid currentColor";
}

function JumpForm0(value) {
    let loopEnd = false;
    var form0Iterations = 0;
    let layerUnderline = "page"
    while (!loopEnd) {
        var myEle = document.getElementById("content" + form0Iterations);
        var layerPage = document.getElementById(layerUnderline + form0Iterations);
        if (myEle) {
            myEle.style.display = "none";
            if (layerPage) {
                layerPage.style.borderBottom = "";
            }
            form0Iterations++;
        }
        else {
            loopEnd = true;
            form0Iterations--;
        }
    }
    console.log('Loading Layer ' + formIteration);
    let formlayer = "content";
    formIteration = value;
    if (formIteration > 0) {
        document.getElementById("back").style.display = "inline-flex";
    }
    else {
        document.getElementById("back").style.display = "none";
    }
    if (formIteration == form0Iterations) {
        document.getElementById("next_txt").innerHTML = "Validar e Finalizar";
    }
    else {
        document.getElementById("next_txt").innerHTML = "Guardar e Avançar";
    }
    let nextForm = formlayer + formIteration;
    document.getElementById(nextForm).style.display = "block";
    let nextLayerLine = layerUnderline + formIteration;
    document.getElementById(nextLayerLine).style.borderBottom = "2px solid currentColor";
}

function SubmitForm0(s, e) {
    //document.getElementById("load_ck_GAI").style.display = "initial";
    var path = window.location.pathname;
    var page = path.split("/").pop();
    var corepage = path.split('?', 1).join();
    let params = (new URL(document.location)).searchParams;
    var attempt = params.get("attempt")
    $.post('/Forms/Form0_Layer' + formIteration + '?attempt='+attempt, $("#Layer" + formIteration).serialize(), function (result) {

    });
}

function FinishAttempt(s, e) {
    //document.getElementById("load_ck_GAI").style.display = "initial";
    if (confirm("Tem a certeza que pretende submeter?")) {
        $.post('/Forms/Form0_Submit', function (result) {
            console.log(result);
            if (result == 'True') {
                var link = '/Forms';
                window.location.href = link;
            }
            else {
                alert("Ocorreu um erro!");
            }
        });
    }

}

function SendFilesAndForm() {
    let params = (new URL(document.location)).searchParams;
    var attempt = params.get("attempt")
    var title = document.getElementById("fileVal");
    var title = document.getElementById("fileVal");
    //$.post('/Forms/Form0_Layer6?attempt=' + attempt, $("#Layer6").serialize(), function (data) {
    //    document.getElementById("fileVal").innerHTML = data;
    //});

    //var formData = new FormData();

    //formData.append("username", "Groucho");
    //formData.append("accountnum", 123456); // number 123456 is immediately converted to a string "123456"

    //// HTML file input, chosen by user
    //formData.append("userfile", fileInputElement.files[0]);

    //// JavaScript file-like object
    //var content = '<a id="a"><b id="b">hey!</b></a>'; // the body of the new file...
    //var blob = new Blob([content], { type: "text/xml" });

    //formData.append("webmasterfile", blob);

    //var request = new XMLHttpRequest();
    //request.open("POST", "http://foo.com/submitform.php");
    //request.send(formData);
}

function openClassTabs(evt, tabname) {
    // Declare all variables
    var i, tabcontent, tablinks;

    // Get all elements with class="tabcontent" and hide them
    tabcontent = document.getElementsByClassName("tabcontent");
    for (i = 0; i < tabcontent.length; i++) {
        tabcontent[i].style.display = "none";
    }

    // Get all elements with class="tablinks" and remove the class "active"
    tablinks = document.getElementsByClassName("tablinks");
    for (i = 0; i < tablinks.length; i++) {
        tablinks[i].className = tablinks[i].className.replace(" active", "");
    }

    // Show the current tab, and add an "active" class to the button that opened the tab
    document.getElementById(tabname).style.display = "block";
    evt.currentTarget.className += " active";
}