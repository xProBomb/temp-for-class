
const namesTextarea = document.getElementById('names');
if (namesTextarea) {
    namesTextarea.addEventListener('input', function () {
        var regex = /^[a-zA-Z ,.\-_'\n\r]+$/;
        if (!regex.test(namesTextarea.value)) {
            namesTextarea.setCustomValidity("Please use only letters, spaces, and the characters ,.-_'");
        } else {
            namesTextarea.setCustomValidity("");
        }
    });
}

const changeTeamNamesButton = document.getElementById("changeTeamNames");
if (changeTeamNamesButton) {
    changeTeamNamesButton.addEventListener("click", function () {

        fetch('/TSwift.txt')
            .then(response => {
                if (!response.ok) {
                    throw new Error('Network response was not ok');
                }
                return response.text();
            })
            .then(data => {
                const names = data.split(/\r?\n/).filter(name => name.trim() !== '');

                const teamNames = document.querySelectorAll('.team-name');
                teamNames.forEach(function (teamName) {
                    const randomName = names[Math.floor(Math.random() * names.length)];
                    teamName.textContent = randomName;
                });
            })
            .catch(error => console.error('Error fetching the names:', error));
    });
}