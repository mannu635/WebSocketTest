﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="AspNetChat.Index" %>

<!DOCTYPE html>
<script src="https://code.jquery.com/jquery-3.1.1.min.js"
        integrity="sha256-hVVnYaiADRTO2PzUGmuLJr8BLUSjGIZsDYGmIJLv2b8="
        crossorigin="anonymous"></script>
<link href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-BVYiiSIFeK1dGmJRAkycuHAHRg32OmUcww7on3RYdg4Va+PmSTsz/K68vbdEjh4u" crossorigin="anonymous">
<script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js" integrity="sha384-Tc5IQib027qvyjSMfHjOMaLkfuWVxZxUPnCJA7l2mCWNIpG9mGCD8wGNIcPD7Txa" crossorigin="anonymous"></script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <script type="text/javascript">
    var ws;
    function $(id) {
      return document.getElementById(id);
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

    function connect() {
      wireEvents();
      var conversation = $('conversation');
      var name = $('name').value;
      var url = 'ws://localhost:10807/chat.ashx?username=' + $('name').value;
      ws = new WebSocket(url);

      ws.onerror = function (e) {
        console.log(e);
        conversation.appendChild(createSpan('Problem with connection: ' + e.message));
      };

      ws.onopen = function () {
        $('beforeconnect').style.display = "none";
        $('afterconnect').style.display = "block";
        $('connected').innerText = $('name').value + " Connected."
      };

      ws.onmessage = function (e) {
        console.log(e);
        conversation.appendChild(createSpan(e.data.toString()));
        conversation.scrollTop = conversation.scrollHeight;
      };

      ws.onclose = function () {
        $('beforeconnect').style.display = "block";
        $('afterconnect').style.display = "none";
        conversation.innerHTML = "";
        $('connected').innerText = "";
      };

    };

    function send() {
      var message = $('message');
      ws.send(message.value);
      message.value = '';
    }
  </script>
  <style>
    .container {
      margin-top: 10px;
      border: 5px solid black;
      
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
        height:100%
    }
    .ctitle{
        background-color:white;
        
    }
  </style>
</head>
<body>

    <form id="form1" runat="server">

  <div class="container">
      <div class="col-md-4 consolebox">
          <span class="text-center ctitle">Console</span>
          
      </div>
      <div class="col-md-4 chatbox">
    <div class="jumbotron text-center">
      <h3>WebSockets Test</h3>
    </div>
    <div class="text-center">
      <div id="beforeconnect" style="display:block">
        <input id="name" placeholder="Name" />
        <input id="connect" type="button" value="Connect" onclick="connect()" />
      </div>
      <div id="connected"></div>
    </div>
    <div id="afterconnect" style="display:none">
      <input id="message" placeholder="Message" />
      <input id="send" type="button" value="Send" onclick="send()" />
      <input id="close" type="button" value="Close Connection" />
    </div>
    <br />
    <div id="conversation" style="overflow-y: scroll;">
    </div>
          </div>
      <div class="col-md-4"></div>
  </div>


    </form>


</body>
</html>
