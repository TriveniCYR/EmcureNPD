function exportPDF(s1, s2) {

 // var doc = new jsPDF('p', 'pt', 'Letter');
  var doc = new jsPDF('p', 'pt', 'a4');
    //A4 - 595x842 pts
    //https://www.gnu.org/software/gv/manual/html_node/Paper-Keywords-and-paper-size-in-points.html


    //Html source
    var source = document.getElementById(s1).innerHTML;

    var margins = {
        top: 10,
        bottom: 10,
        left: 80,
      width: 475
    };

    doc.fromHTML(
        source, // HTML string or DOM elem ref.
        margins.left,
        margins.top, {
            'width': margins.width,
            'elementHandlers': specialElementHandlers
        },

        function (dispose) {
            // dispose: object with X, Y of the last line add to the PDF
            //          this allow the insertion of new lines after html
            doc.save(s2);
        }, margins);
}