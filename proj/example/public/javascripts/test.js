const listForm = document.querySelector("#listForm"),
            input = listForm.querySelector("input"),
            insert_id = document.querySelector("#id"),
            insert_pw = document.querySelector("#pw"),
            insert_name = document.querySelector("#name"),
            insert_mail = document.querySelector("#mail"),
            insert_yy = document.querySelector("#yy"),
            insert_mm = document.querySelector("#mm"),
            insert_dd = document.querySelector("#dd"),
            btnJoin = document.querySelector("#btnJoin");

        


        function id_check(inputId,inputPW,inputNAME,inputMAIL,inputY,inputM,inputD) {
            $.ajax({
                url: '/api/id_check',
                dataType: "json",
                type: "POST",
                data: {
                    input_id : inputId,
                    input_pw : inputPW,
                    input_name : inputNAME,
                    input_mail : inputMAIL,
                    input_yy : inputY,
                    input_mm : inputM,
                    input_dd : inputD
                },
                success: (res) => {
                    if(res.length == 1){
                        console.log(res);
                        console.log(res[0].id);
                        console.log(`${inputId} ` + "is exist")
                        console.log("등록 성공");
                    }
                    else{
                        console.log("no");
                    }
                },
                error: (err) => {
                    console.log(err)
                }
            })
        }

        function submitList(e) {  
            e.preventDefault();   // 새로고침을 안함
            const insert_id = insert_id.value,   // 입력창에 현재 입력된 값을 저장
                  insert_pw = insert_pw.value,
                  insert_name = insert_name.value,
                  insert_mail = insert_mail.value,
                  insert_yy = insert_yy.value,
                  insert_mm = insert_mm.value,
                  insert_dd = insert_dd.value;

            id_check(inputId); // 저장된 텍스트를 리스트의 함수 인자로 보냄
            input.value = ""; // 입력창 빈칸으로 초기화
        }

        function init() {
           listForm.addEventListener("submit", submitList); 
        }
        init();