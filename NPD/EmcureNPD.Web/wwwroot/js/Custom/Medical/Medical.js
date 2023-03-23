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
	const myDivs = fileList.getElementsByClassName('my-div');
	while (myDivs.length > 0) {
		myDivs[0].parentNode.removeChild(myDivs[0]);
	}
	var i = 0;
	//const dataTransfer = new DataTransfer();
	//Array.from(input.files).forEach((file, i) => {
	//		dataTransfer.items.add(file);
	//});
	//input.files = dataTransfer.files;
	for (const file of input.files) {
		const div = document.createElement("div");
		div.setAttribute("id", "elements_" + i);
		div.classList.add('my-div');
		const del = document.createElement("button");
		del.type = "button";
		del.classList.add("btn", "btn-sm", "btn-danger", "ml-3", "mb-2");
		del.textContent = "Delete";
		del.onclick = () => deleteSelectedFile(div.id);

		const a = document.createElement("a");
		a.textContent = file.name;
		a.href = "#";
		a.style.textDecoration = "none";
		a.addEventListener("click", () => previewFile(file));
		div.appendChild(a);
		div.appendChild(del);
		fileList.appendChild(div);
		i++;
	}
});
//delete selected files
function deleteSelectedFile(id) {
	const element = document.getElementById(id);
	const index = parseInt(id.replace("elements_", ""), 10);
	const dataTransfer = new DataTransfer();
	Array.from(input.files).forEach((file, i) => {
		if (i !== index) {
			dataTransfer.items.add(file);
		}
	});
	input.files = dataTransfer.files;
	element.remove();
}

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