<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="AspNetChat.Index" %>

<!DOCTYPE html>
<html>
<script src="https://code.jquery.com/jquery-3.1.1.min.js"
        integrity="sha256-hVVnYaiADRTO2PzUGmuLJr8BLUSjGIZsDYGmIJLv2b8="
        crossorigin="anonymous"></script>
<link href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-BVYiiSIFeK1dGmJRAkycuHAHRg32OmUcww7on3RYdg4Va+PmSTsz/K68vbdEjh4u" crossorigin="anonymous">
<script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js" integrity="sha384-Tc5IQib027qvyjSMfHjOMaLkfuWVxZxUPnCJA7l2mCWNIpG9mGCD8wGNIcPD7Txa" crossorigin="anonymous"></script>

<head>
  <script type="text/javascript">
    var ws;
    function $(id) {
      return document.getElementById(id);
    }
    function consolelog(string) {
        var consolelog = $('myPanel');
        consolelog.appendChild(createSpan(string));
        consolelog.scrollTop = conversation.scrollHeight;

    }
    function wireEvents() {
      $('close').addEventListener('click', function () {
        ws.close();
      });
    }
    function createSpan(text) {
      var span = document.createElement('span');
      span.innerHTML = text + '<br />';
      return span;
    }

    function connectuser() {
      wireEvents();
      var conversation = $('conversation'); 
      var name = $('name').value;
        var url = 'ws://workliowebsocket.azurewebsites.net/chat.ashx?username=' + $('name').value;
      //var url = 'ws://localhost:10807/chat.ashx?username=' + $('name').value;
      ws = new WebSocket(url);

      ws.onerror = function (e) {
        console.log(e);
        conversation.appendChild(createSpan('Problem with connection: ' + e.message));
      };

      ws.onopen = function () {
        $('beforeconnect').style.display = "none";
        $('afterconnect').style.display = "block";
        $('afterconnect').focus();
        $('connected').innerText = $('name').value + " Connected."
      };

      ws.onmessage = function (e) {
          
          var data = e.data.toString();

          if (data.charAt(0) == '~') {
              consolelog(data.substr(1, data.length));
              consolelog.scrollTop = consolelog.scrollHeight;
          }
          else {
              conversation.appendChild(createSpan(data));
              conversation.scrollTop = conversation.scrollHeight;
          }
      };

      ws.onclose = function () {
        $('beforeconnect').style.display = "block";
        $('afterconnect').style.display = "none";
        conversation.innerHTML = "";
        $('connected').innerText = "";
      };

    };

    function sendmsg() {
      var message = $('message');
      ws.send(message.value);
      message.value = '';
    }
  </script>
  <style>
    .container {
      margin-top: 10px;
      border: 5px solid black;
      padding:0px!important;
      
    }
    .chatbox{
         border: 2px solid black;
         padding: 4px;
    }
    #afterconnect {
      position: fixed;
      bottom: 0;
      padding: 20px;
      margin-bottom: 10px;
      background-color: gray;
      left: 50%;
      transform: translateX(-50%);
    }

    #conversation {
      border: 1px solid black;
      height: 400px;
      padding: 10px;
      background-color: ghostwhite;
    }
    .jumbotron{
        background-color:#89d0df;
    }
    .consolebox{
        background-color:black;
        height: 500px;
        border:5px solid white;
        padding:5px!important;
        
    }
    .ctitle{
        background-color:white;
        width:100%;
        
    }
    h3{
        margin-top:1px!important;
        margin-bottom:5px!important;
    }
  </style>
</head>
<body>

    <form id="form1" runat="server">

  <div class="container">
      <div class="col-sm-4 consolebox">
          <h3 class="text-center ctitle"> Console</h3>
          <div id="myPanel" style="color:Lime;  overflow-y: scroll; height: 450px;"  runat="server">                
            </div>
          
      </div> 
     

      <div class="col-sm-4 chatbox">
    <div class="jumbotron text-center">
      <h3>WebSockets Test</h3>
    </div>
    <div class="text-center">
      <div id="beforeconnect" style="display:block">
        <input id="name" placeholder="Name" onkeypress="if (event.keyCode==13) {connectuser()}"/>
        <input id="connect" type="button" value="Connect" onclick="connectuser()" />
      </div>
      <div id="connected"></div>
    </div>
    <div id="afterconnect" style="display:none">
      <input id="message" placeholder="Message" onkeypress="if (event.keyCode==13){ sendmsg()}"/>
      <input id="send" type="button" value="Send" onclick="sendmsg()" />
      <input id="close" type="button" value="Close Connection" />
    </div>
    <br />
    <div id="conversation" style="overflow-y: scroll;">
    </div>
     </div>
      <div class="col-sm-4" style="text-align: center; padding-top:50px;">
         
          <div class="panel-body">
            Click the button below to switch between Save/Access to SQL and Save/Access to Cache.
          </div>
          <strong>Current Configuration: </strong><asp:Label runat="server" ID="lbl" Text="Cache"></asp:Label>
         <asp:Button id="switchsrc" runat="server" CssClass="btn-block btn-primary" Text="Switch" OnClick="switchsrc_Click"  TabIndex="5" UseSubmitBehavior="False" AutoPostback = false/>
        </div>
  </div>


    </form>


</body>
</html>
