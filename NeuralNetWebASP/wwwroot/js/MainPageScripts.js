var PassOneGenerationBTN = document.getElementById("PassOneGenerationBTN");
var SeveralGenerationsPass = document.getElementById("SeveralGenerationsPass");
var addElementButton = document.getElementById("addElementButton");
var LearnUntillStop = document.getElementById("LearnUntillStop");
var StopLearningButton = document.getElementById("StopLearningButton");
// Hide form id generation exists
function Init() {
    fetch(`api/neuralnets/generationisnull`)
        .then(response => response.json())
        .then(data => {
            if (data.isnull == false) document.getElementById("CreateNNform").innerHTML = "";
        })
        .catch(error => {
            alert("Error has occured " + error);
        });
}
Init();
// Passing several generations
SeveralGenerationsPass.onclick = function () {
    LearnButtonsShow(false);
    var num = document.getElementById("gens_amount").value;
    fetch(`api/neuralnets/passseveralgenerations`, { method: 'POST', headers: { 'Content-type': 'application/json' }, body: JSON.stringify(num) })
        .then(response => response.json())
        .then(data => {
            RefreshBaseData();
            LearnButtonsShow(true);
        })
        .catch(error => { alert(error); });
}
// Shows window with fields that user uses to adding new examples to learning database of neural net
addElementButton.onclick = function () {
    fetch(`api/neuralnets/getaddingtodbview`)
        .then(response => response.text())
        .then(data => {
            document.getElementById("AddElementToDatabase").innerHTML = data;
            SetFormAddToDB();
        })
        .catch(error => {
            document.getElementById("AddElementToDatabase").innerHTML = `<p style='color: red;'>Error: ${error}</p>`;
            alert("Error: " + error);
        });
}
// Adds event listener to form, when it shows to user
function SetFormAddToDB() {
    document.getElementById('myForm').addEventListener('submit', function (event) {
        event.preventDefault();
        var inputs = [];
        var outputs = [];
        var num = 0;
        while (document.getElementById(`input_${num}`) != undefined) {
            var value = document.getElementById(`input_${num}`).value;
            inputs.push(value);
            num++;
        }
        num = 0;
        while (document.getElementById(`output_${num}`) != undefined) {
            var value = document.getElementById(`output_${num}`).value;
            outputs.push(value);
            num++;
        }
        // sending request
        fetch(`api/neuralnets/addtodb`, {
            method: 'POST',
            headers: { 'Content-type': 'application/json' },
            body: JSON.stringify({ LearningInputs: inputs, ExpectedOutputs: outputs })
        })
            .then(response => response.text())
            .then(data => {
                document.getElementById("AddingToDBresult").innerHTML = data;
            })
            .catch(error => { console.log(error); });
    });
}
PassOneGenerationBTN.onclick = function () {
    LearnButtonsShow(false);
    fetch(`api/neuralnets/passgeneration`, { method: 'POST', headers: { 'Content-type': 'application/json' } })
        .then(response => response.json())
        .then(data => {
            RefreshBaseData();
            LearnButtonsShow(true);
        })
        .catch(error => { console.log(error); });
}
function ChangeLF(increase) {
    fetch(`api/neuralnets/changelearningfactor`, { method: 'POST', headers: { 'Content-Type': 'application/json' }, body: JSON.stringify(increase) })
        .then(response => response.text())
        .then(data => {
            console.log(data);
            fetch(`api/neuralnets/gendata`)
                .then(response => response.json())
                .then(data => { RefreshBaseData(); })
                .catch(error => { console.log(error); });
        })
        .catch(error => { console.log(error); });
}
function StartLearning() {
    LearnButtonsShow(false);
    fetch(`api/neuralnets/start_learning`, { method: 'POST', headers: { 'Content-type': 'text/plain' } })
        .then(response => response.text())
        .then(data => { console.log(data); })
        .catch(error => { console.log(error); });
}
function StopLearning() {
    fetch(`api/neuralnets/stop_learning`, { method: 'POST', headers: { 'Content-type': 'text/plain' } })
        .then(response => response.text())
        .then(data => { console.log(data); })
        .catch(error => { console.log(error); });
    LearnButtonsShow(true);
}
function FillByRandomNumbers() {
    var num = 0;
    while (document.getElementById(`input_${num}`) != undefined) {
        document.getElementById(`input_${num}`).value = randomNumber();
        num++;
    }
    num = 0;
    while (document.getElementById(`output_${num}`) != undefined) {
        document.getElementById(`output_${num}`).value = randomNumber();
        num++;
    }
}
function randomNumber() {
    return (Math.random() * 2) - 1;
}
function RefreshBaseData() {
    fetch(`api/neuralnets/gendata`)
        .then(response => response.json())
        .then(data => {
            document.getElementById("BaseData").innerHTML = `Database size: ${data.databaseSize}<br>Generations passed: ${data.generationsPassed}<br>Current error: ${data.currentError}<br>Change error: ${data.changeError}<br>Learning factor: ${data.learningFactor}`;
            console.log(data);
        })
        .catch(error => { console.log(error); });
}
function LearnButtonsShow(enabled) {
    PassOneGenerationBTN.disabled = !enabled;
    SeveralGenerationsPass.disabled = !enabled;
    LearnUntillStop.disabled = !enabled;
}