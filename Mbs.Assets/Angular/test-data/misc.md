<p>Noting the presence of a footnote<sup><a href="#note1">1</a></sup> is one common way for superscripts to be used.</p> 
<p><strong>Footnotes:</strong></p> 
<p id="note1"><sup>1</sup> A footnote is an explanatory comment placed at the bottom of a page and linked to from the location
within the page where the information contained in the note applies.</p>
====================================================
https://github.com/rauschma/html_demos
----------------------
footnotes.css
a.footptr {
    vertical-align: super;
    font-size: .83em;
    text-decoration: none;
}
a.refptr {
    text-decoration: none;
}
.foot-tooltip {
    background-color: #FCF6CF;
    padding-top: 0.5em;
    padding-bottom: 0.5em;
    padding-left: 1em;
    opacity: 0;
	-webkit-transition: opacity 0.3s linear;
	-moz-transition: opacity 0.3s linear;
	-o-transition: opacity 0.3s linear;
	-ms-transition: opacity 0.3s linear;
	transition: opacity 0.3s linear;
}

li:target {
    background-color: #BFEFFF;
}

ol#references {
    list-style:none;
	margin-left: 0;
	padding-left: 1.8em;
	text-indent: -1.8em;

    counter-reset: refcounter;
}
ol#references > li:before {
  content: "[" counter(refcounter) "] ";
  counter-increment: refcounter;
}

ol#footnotes {
    list-style:none;
	margin-left: 0;
	padding-left: 1.8em;
	text-indent: -1.8em;

    counter-reset: footcounter;
}
ol#footnotes > li:before {
  content: "(" counter(footcounter) ") ";
  counter-increment: footcounter;
}
-----------------------
footnotes.js
(function () {
    var FOOTNOTE_REGEX = /^\([0-9]+\)$/;
    var REFERENCE_REGEX = /^\[[0-9]+\]$/;
    
    var oldOnLoad = window.onload;
    window.onload = function (event) {
        if (document.getElementsByClassName) {
            var elems = document.getElementsByClassName("ptr");
            for (var i = 0; i<elems.length; i++) {
                var elem = elems[i];
                var ptrText = elem.innerHTML;
                if (FOOTNOTE_REGEX.test(ptrText)) {
                    elem.className = "ptr footptr";
                    elem.onclick = toggle;
                } else if (REFERENCE_REGEX.test(ptrText)) {
                    elem.className = "ptr refptr";
                }
                elem.setAttribute("href", "#"+ptrText);
            }
            addListItemIds("references", "[", "]");
            addListItemIds("footnotes", "(", ")");
        }

        if (typeof oldOnLoad === "function") {
            oldOnLoad(event);
        }
    };
    
    function addListItemIds(parentId, before, after) {
        var refs = document.getElementById(parentId);
        if (refs && refs.getElementsByTagName) {
            var elems = refs.getElementsByTagName("li");
            for (var i = 0; i<elems.length; i++) {
                var elem = elems[i];
                elem.setAttribute("id", before+(i+1)+after);
            }
        }
    }
    
    var currentDiv = null;
    var currentId = null;
    function toggle(event) {
        var parent = this.parentNode;
        if (currentDiv) {
            parent.removeChild(currentDiv);
            currentDiv = null;
        }
        var footnoteId = this.innerHTML;
        if (currentId === footnoteId) {
            currentId = null;
        } else {
            currentId = footnoteId;
            currentDiv = document.createElement("div");
            var footHtml = document.getElementById(footnoteId).innerHTML;
            currentDiv.innerHTML = footHtml;                        
            currentDiv.className = "foot-tooltip";
            parent.insertBefore(currentDiv, this.nextSibling);
            setTimeout(function () {
                currentDiv.style.opacity = "1";
            }, 0);
        }
        event.preventDefault();
    }
}());
---------------------------------------
<!doctype html>
<html>
    <head>
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
        <title>JavaScript-enhanced footnotes and references</title>
        <link rel="stylesheet" href="footnotes.css" type="text/css">
        <script src="footnotes.js"></script>
    </head>
    <body>
        <h1>JavaScript-enhanced footnotes and references</h1>
        
        <h2>About</h2>
        
        Challenges:
        <ul>
        <li>On screen, show footnote content close to where the footnote is referenced.</li>
        <li>Support touch devices.</li>
        <li>In print, show footnotes as usual.</li>
        <li>Degrade gracefully if JavaScript is switched off.</li>
        </ul>
        More information:
        <ul>
            <li>Companion blog post: “<a href="http://www.2ality.com/2011/12/footnotes.html">Handling footnotes and references in HTML</a>”</li>
            <li>Project <a href="https://github.com/rauschma/html_demos">html_demos</a> on GitHub</li>
        </ul>

        <h2>Demonstration</h2>
        
        You can use an IIFE<a class="ptr">(1)</a> to avoid the global namespace<a class="ptr">(2)</a> being polluted. JavaScript has many functional language constructs <a class="ptr">[1]</a>. For example: consult <a class="ptr">[2]</a> for an introduction to closures.

        <h2>Footnotes</h2>

        <ol id="footnotes">
            <li>IIFE is an acronym for Immediately-Invoked Function Expression. Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.
            </li>
            <li>The global scope is reified as an object in JavaScript. A <a href="http://www.example.com">link</a> to somewhere else.
                One more line.
            </li>
        </ol>
        
        <h2>References</h2>
        
        <ol id="references">
            <li><a href="http://en.wikipedia.org/wiki/Functional_programming"><i>Functional programming</i></a>. In <i>Wikipedia</i>. Retrieved 2011-12-03.
            </li>
            <li>Douglas Crockford, <i>JavaScript: The Good Parts</i>. O’Reilly. 2008-05-16.</li>
        </ol>
    </body>
</html>
=====================================================
options: ["Circle", "Triangle", "Diamond" ,"Square", "Pentagon", "Hexagon"]
{
  mutable run=0;
  const svg = d3.select(DOM.svg(width,height));
 
  const voronoi = svg.append("g");
  const labels = svg.append("g");
  const pop_labels = svg.append("g");
  

  let seed = new Math.seedrandom(20);
  let voronoiTreeMap = d3.voronoiTreemap()
    .prng(seed)
    .clip(shape);

  voronoiTreeMap(dataHierarchy);  


  let allNodes = dataHierarchy.descendants()
    .sort((a, b) => b.depth - a.depth)
    .map((d, i) => Object.assign({}, d, {id: i}));
  

  voronoi.selectAll('path')
    .data(allNodes)
    .enter()
    .append('path')
    .attr('d', d => "M" + d.polygon.join("L") + "Z")
    .attr("stroke", "#F5F5F2")
    .style('fill-opacity', d => d.depth === 2 ? 1 : 0)
    .attr("class","path")
     .attr("stroke-width", d => 7 - d.depth*2.8);
  
   mutable run = 1;

  return svg.node();
  
}
=========================
external link css:

a[href ^= "http"]:after {
  content: " " url(data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAoAAAAKCAYAAACNMs+9AAAAVklEQVR4Xn3PgQkAMQhDUXfqTu7kTtkpd5RA8AInfArtQ2iRXFWT2QedAfttj2FsPIOE1eCOlEuoWWjgzYaB/IkeGOrxXhqB+uA9Bfcm0lAZuh+YIeAD+cAqSz4kCMUAAAAASUVORK5CYII=);    
}

the icon above is blue, for a monochrome icon use this: data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAoAAAAKCAYAAACNMs+9AAAAGXRFWHRTb2Z0d2FyZQBBZG9iZSBJbWFnZVJlYWR5ccllPAAAAIpJREFUeNqEkIEJwCAMBGPpEs7hHK6hY+gauoZr6Bo6RtqEWkxb6EMQnuM/USEirIoxSmOKwDkhBEwp4VPkb4801Xun9xW4e+9FldYarLUvkBPPOp4JGWOgtSaSt6VWQKUU9nPOEiSIgAmNMdadFTjn7utqrXwh6fLvH9nXhamW5ksMnpfDnw4BBgBfunO056MmqAAAAABJRU5ErkJggg==


combined @superlogical + @Shaz to make something for my Foundation WP theme...
a[href^="http://"]:not([href*="maggew.com"]):after {
     content: " " url(data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAoAAAAKCAYAAACNMs+9AAAAVklEQVR4Xn3PgQkAMQhDUXfqTu7kTtkpd5RA8AInfArtQ2iRXFWT2QedAfttj2FsPIOE1eCOlEuoWWjgzYaB/IkeGOrxXhqB+uA9Bfcm0lAZuh+YIeAD+cAqSz4kCMUAAAAASUVORK5CYII=);    
}
can use this HTML snippet to test on your site:
<a href="http://gebfire.com" target="_blank">External</a>

To include a neat icon you can place an SVG in a CSS variable and then use it in your CSS rule.
    --icon-external-link: url('data:image/svg+xml,\
        <svg xmlns="http://www.w3.org/2000/svg" viewBox="-24 -24 48 48"> \
            <defs> \
                <mask id="corner"> \
                    <rect fill="white" x="-24" y="-24" width="48" height="48"></rect> \
                    <rect fill="black" x="2" y="-24" width="22" height="26"></rect> \
                </mask> \
            </defs> \
            <g stroke="blue" fill="blue" stroke-width="4"> \
                <rect x="-20" y="-16" width="32" height="32" rx="7" ry="7" stroke-width="3" fill="none" mask="url(%23corner)"/> \
                <g transform="translate(1,0)" stroke-linecap="square"> \
                    <line x1="0" y1="0" x2="17" y2="-17" stroke-width="6"/> \
                    <polygon points="21 -21, 21 -8, 8 -21" stroke-linejoin="round" stroke-width="3"/> \
                 </g> \
             </g> \
        </svg>');
Then in your rule you can reference it like so:
a[href^="http"]:not([href*="example.mil"])::after {
    content: '';
    background: no-repeat left .25em center var(--icon-external-link);
    padding-right: 1.5em;
}
You may also want to be strict about how you open external links. For instance you might want to always have external links open in a new tab with the target blank approach. To do this properly you need to include target="_blank" as well as rel="noopener noreferrer" in your links.

Suppose you are setting the target for external links by <a href='http://' target='_blank'>Justin Bieber Fan Club</a> , You can do play:
a[target="_blank"]:after{
     content: " [external]" 
}
============================
