

// Question-1: Multiplication & Division
function Multiply() {
    let num1 = parseFloat(document.getElementById('num1').value); // converting string input to float
    let num2 = parseFloat(document.getElementById('num2').value);
    let multiplication = num1 * num2;
    document.getElementById('result').innerHTML = "Multiplication: " + multiplication;
}

function Divide() {
    let num1 = parseFloat(document.getElementById('num1').value);
    let num2 = parseFloat(document.getElementById('num2').value);
    let division = num2 !== 0 ? num1 / num2 : "Division by zero!";
    document.getElementById('result').innerHTML = "Division: " + division;
}

// Question-2: Odd/Even Check
function OddEvenCheck() {
    let output = "";
    for (let i = 0; i <= 15; i++) {
        output += `${i} is ${i % 2 === 0 ? "even" : "odd"}<br>`;
    }
    document.getElementById('oddEvenOutput').innerHTML = output;
}

// Quesion-3: Button Click Event
document.getElementById('MyBtn').addEventListener("click", function () {
    console.log("Button was clicked!");
    alert("Button was clicked!");
}); 

// Question-4: Hide Odd Rows of Table
function HideOddRows() {
    $("#MyTable tr:odd").hide();
}

// Question-5: Apply Background to h1 in div
function HighlightDivH1() {
    $("div > h1").addClass("highlight");
}

// Question-6: jQuery Code to Get a Second Element
function SelectSecond() {
    $("li").eq(1).css("color", "red");
}

// Question-7: Add Index to List Items
function AddIndex() {
    $("#MyList li").each(function (index) {
        $(this).prepend(index + " ");
    });
}

// Question-8: Append <p> when input changes
$("#InputChanged").on("change", function () {
    $(this).after("<p>Input changed!</p>");
});