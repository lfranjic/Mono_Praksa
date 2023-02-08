    const form = document.getElementById("personForm");
    const submissionsBody = document.getElementById("submissionsBody");

    function addSubmission(name, email, gender) {
        const tr = document.createElement("tr");
        const nameTd = document.createElement("td");
        nameTd.textContent = name;
        tr.appendChild(nameTd);
        const emailTd = document.createElement("td");
        emailTd.textContent = email;
        tr.appendChild(emailTd);
        const genderTd = document.createElement("td");
        genderTd.textContent = gender;
        tr.appendChild(genderTd);
        submissionsBody.appendChild(tr);

        const list = JSON.parse(localStorage.getItem("list")) || [];
        list.push({ name, email, gender });
        localStorage.setItem("list", JSON.stringify(list));
        console.log(list);
    }

    form.addEventListener("submit", function(event) {
        event.preventDefault();
        const name = document.getElementById("name").value;
        const email = document.getElementById("email").value;
        const gender = document.querySelector('input[name="gender"]:checked').value;
        addSubmission(name, email, gender);
  });