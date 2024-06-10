document.addEventListener("DOMContentLoaded", function () {
    let deleteBtns = document.querySelectorAll("#deleteBtn");

    const hubLikeConnection = new signalR.HubConnectionBuilder()
        .withUrl("/likeHub")
        .build();
    const hubCommentConnection = new signalR.HubConnectionBuilder()
        .withUrl("/commentHub")
        .build();


    hubLikeConnection.on("ReceiveLikesUpdate", function (itemId, likesCount) {
        let itemElement = document.querySelector(`[data-item-id='${itemId}']`);
        if (itemElement) {
            let likesCountElement = itemElement.querySelector("#likesCount");
            if (likesCountElement) {
                likesCountElement.textContent = likesCount;
            }
        }
    });

    hubCommentConnection.on("ReceiveCommentUpdate", function (commentText, userName, commentId, isAdmin, isOwner) {
        let commentContainer = document.getElementById("comments");
        let newCommentDiv = document.createElement("div");
        newCommentDiv.classList.add("mb-1", "p-1", "border", "rounded-1");
        newCommentDiv.id = `comment-${commentId}`;
        let headerDiv = document.createElement("div");
        headerDiv.classList.add("d-flex", "justify-content-between", "align-items-center");
        let userNameEm = document.createElement("em");
        userNameEm.textContent = `${userName}:`;
        console.log(isAdmin);
        console.log(isOwner);
        let deleteBtn = document.createElement("a");
        deleteBtn.id = "deleteBtn";
        headerDiv.appendChild(userNameEm);


        deleteBtn.href = `/Comment/DeleteComment?commentId=${commentId}`;
        deleteBtn.innerHTML = `
    <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-bookmark-x" viewBox="0 0 16 16">
        <path fill-rule="evenodd" d="M6.146 5.146a.5.5 0 0 1 .708 0L8 6.293l1.146-1.147a.5.5 0 1 1 .708.708L8.707 7l1.147 1.146a.5.5 0 0 1-.708.708L8 7.707 6.854 8.854a.5.5 0 1 1-.708-.708L7.293 7 6.146 5.854a.5.5 0 0 1 0-.708" />
        <path d="M2 2a2 2 0 0 1 2-2h8a2 2 0 0 1 2 2v13.5a.5.5 0 0 1-.777.416L8 13.101l-5.223 2.815A.5.5 0 0 1 2 15.5zm2-1a1 1 0 0 0-1 1v12.566l4.723-2.482a.5.5 0 0 1 .554 0L13 14.566V2a1 1 0 0 0-1-1z" />
    </svg>`;
        if (isAdmin || isOwner) {
            headerDiv.appendChild(deleteBtn);
        }


        let textDiv = document.createElement("div");
        textDiv.style.marginLeft = "30px";
        textDiv.textContent = commentText;
        newCommentDiv.appendChild(headerDiv);
        newCommentDiv.appendChild(textDiv);
        commentContainer.appendChild(newCommentDiv);

        deleteBtn.addEventListener("click", function (e) {
            e.preventDefault();
            let deleteUrl = this.href;
            console.log(deleteUrl);
            fetch(deleteUrl)
                .then(response => response.text())
                .catch(error => console.error('Error:', error));
        });
    });

    hubCommentConnection.on("ReceiveCommentDelete", function (commentId) {
        const commentElement = document.getElementById(`comment-${commentId}`);
        if (commentElement) {
            commentElement.remove();
        }
    });

    document.getElementById("commentForm").addEventListener("submit", function (e) {
        e.preventDefault();
        let commentUrl = this.action;
        let formData = new FormData(this);
        document.getElementById("commentArea").value = "";
        fetch(commentUrl, {
            method: 'POST',
            body: formData
        })
            .then(response => response.text())
            .catch(error => console.error('Error:', error));
    });

    document.getElementById("likeBtn").addEventListener("click", function (e) {
        e.preventDefault();
        let likeUrl = this.href;
        //console.log(likeUrl);
        fetch(likeUrl)
            .then(response => response.text())
            .catch(error => console.error('Error:', error));
    });

    deleteBtns.forEach(function (deleteBtn) {
        deleteBtn.addEventListener("click", function (e) {
            e.preventDefault();
            //console.log(`Button clicked: ${this.href}`);
            let deleteUrl = this.href;
            fetch(deleteUrl)
                .then(response => response.text())
                .catch(error => console.error('Error:', error));
        });
    });

    hubLikeConnection.start().catch(err => console.error(err));
    hubCommentConnection.start().catch(err => console.error(err));
});