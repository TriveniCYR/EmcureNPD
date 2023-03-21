$(document).ready(function () {
	IsViewModeMedical();
	_PIDFID = parseInt(pidid);
	getPIDFAccordion(_PIDFAccordionURL, _PIDFID, "dvPIDFAccrdion");
})
function IsViewModeMedical() {
	if ($("#IsView").val() == '1') {
		SetMedicalFormReadonly();
	}
}
function SetMedicalFormReadonly() {
	$(".content").find("input, button, submit, textarea, select").prop('disabled', true);
	$("#cancelButton").prop('disabled', false);
}
// browse files
var selectFileButton = document.getElementById("selectFileButton");
var fileInput = document.getElementById("File");

selectFileButton.addEventListener("click", function () {
	event.preventDefault();
	fileInput.click();
});
// preview Files

function previewFile(file) {
	window.open(URL.createObjectURL(file));
}

const input = document.getElementById("File");
const fileList = document.getElementById("fileList");
input.addEventListener("change", () => {
	fileList.innerHTML = "";
	for (const file of input.files) {
		const li = document.createElement("li");
		const a = document.createElement("a");
		a.textContent = file.name;
		a.href = "#";
		a.addEventListener("click", () => previewFile(file));
		li.appendChild(a);
		fileList.appendChild(li);
	}
});

//remove files
function deleteElement(controlId) {
	var controlName = "FileName_" + controlId + "_";
	$('#' + controlName).val('');
	var divControl = "element_" + controlId;
	var divCtrl = document.getElementById("element_" + controlId)
	divCtrl.style.display = "none";
}

function CancelModal() {
	$('#CancelModel').modal('show');
}