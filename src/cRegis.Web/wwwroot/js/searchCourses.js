// JavaScript source code
$(document).ready(function () {

    var domain = AWS_PROD_DOMAIN;
    
    var autocomplete = new autoComplete({
        data: {                              // Data src [Array, Function, Async] | (REQUIRED)
            src: async () => {
                // API key token
                const token = "this_is_the_API_token_number";
                // User search query
                const query = document.querySelector("#autoComplete").value;
                // Fetch External Data Source
                const source = await fetch(domain+`/course?keywords=${query}`);
                // Format data into JSON
                const data = await source.json();
                // Return Fetched data
                return buildCourseList(data);
            },
            key: ["name"],
            cache: false
        },
        //query: {                               // Query Interceptor               | (Optional)
        //    manipulate: (query) => {
        //        return query.replace("pizza", "burger");
        //    }
        //},
        sort: (a, b) => {                    // Sort rendered results ascendingly | (Optional)
            if (a.match < b.match) return -1;
            if (a.match > b.match) return 1;
            return 0;
        },
        placeHolder: "Course Infomation",     // Place Holder text                 | (Optional)
        selector: "#autoComplete",           // Input field selector              | (Optional)
        threshold: 3,                        // Min. Chars length to start Engine | (Optional)
        //debounce: 300,                       // Post duration for engine to start | (Optional)
        searchEngine: "loose",              // Search Engine type/mode           | (Optional)
        resultsList: {                       // Rendered results list object      | (Optional)
            render: true,
            container: source => {
                source.setAttribute("id", "course_list");
            },
            destination: document.querySelector("#autoComplete"),
            position: "afterend",
            element: "ul"
        },
        maxResults: 20,                         // Max. number of rendered results | (Optional)
        highlight: true,                       // highlight matching results      | (optional)
        resultItem: {                          // Rendered result item            | (Optional)
            content: (data, source) => {
                source.innerHTML = data.match;
            },
            element: "li"
        },
        noResults: () => {                     // Action script on noResults      | (Optional)
            const result = document.createElement("li");
            result.setAttribute("class", "no_result");
            result.setAttribute("tabindex", "1");
            result.innerHTML = "No Results";
            document.querySelector("#course_list").appendChild(result);
        },
        onSelection: feedback => {             // Action script onSelection event | (Optional)
            var id = feedback.selection.value['id'];
            var url = domain + `/course/Detail?cid=${id}`;
            window.location.replace(url);
        }
    });


    function buildCourseList(courseList) {

        try {
            var compactList = [];
            courseList.forEach(function (course) {
                compactList.push({ 'name': course.courseName + ' - ' + course.courseDescription, 'id': course.courseId})
            })
            return compactList;
        } catch (e){
             return courseList;
        }
    
    }



}
)
