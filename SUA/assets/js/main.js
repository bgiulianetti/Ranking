USERID = 0;

$("#logout").click(function(e){
  e.preventDefault();
  e.stopPropagation();
  handle_logout()
})

handle_logout = function(){
  $.ajax({
    url: "/logout",
    type: 'post',
    success: function () {
      var expires = "expires=-1";
      document.cookie = "sessionId=;"+expires+"; path=/";
      window.location = "/login";
    },
    error: function (response) {
      console.log("Error: " + this.url + " " + response.responseJSON.message);
    }
  });
}

setIncideceMenu = function(){
  incidenceMenu = $("#incidenceBucket")
  getIncidences = function(fn){
    $.ajax({
      url: "/incidence/getLastTen",
      type: 'get',
      success: function(response){
        fn(response)
      },
      error: function (response) {
        console.log(response)
      }
    });
  }
  if( typeof incidenceMenu != "undefined" ){
    getIncidences(function(incidences){
      incidenceMenu.find("ul").html(incidences)
      incidenceMenu.find(".toggle").click(function(){
        if(incidenceMenu.hasClass("active")) incidenceMenu.removeClass("active")
        else incidenceMenu.addClass("active")
      })
    })
  }
}
setIncideceMenu()

incidencia = function(userId,textToLog,fn){
  incidence = {
    id: parseInt(userId),
    description: textToLog
  }
  $.ajax({
    url: "/incidence/add",
    type: 'post',
    contentType: "application/json; charset=utf-8",
    data: JSON.stringify(incidence),
    success: function(response){
      fn(response)
    },
    error: function (response) {
      console.log(response)
    }
  });

  console.log("El usuario: "+userId+" hizo -> "+textToLog)
}


setNav = function(){
  selectNav = function(section){
    $(".nav li a ").each(function(){
      $(this).removeClass("active")
      if($(this).attr("data-to") == section) $(this).addClass("active")
      if($(this).attr("data-to") == section && $(this).attr("data-toggle") == "collapse") $(this).click()
    })
  }
  url = window.location.href;
  switch (true) {
    case url.indexOf("/videos/") >= 0: selectNav("videos"); break;
    case url.indexOf("/practices/") >= 0: selectNav("practices"); break;
    case url.indexOf("/geo/") >= 0: selectNav("geo"); break;
    case url.indexOf("/users/") >= 0: selectNav("users"); break;
    case url.indexOf("/microsites/") >= 0: selectNav("microsites"); break;
    default: selectNav("dashboard")
  }

}()

makeList = function(opts){
  // opts = {
  //   prev: $("#videos-list *[data-list='prev']"),
  //   next: $("#videos-list *[data-list='next']"),
  //   loading: $("#videos-list *[data-list='loading']"),
  //   content: $("#videos-list *[data-list='content']"),
  //   link: "/videos/list"
  // }
  this.opts = opts;
  this.opts.prev.click(
    function(t){
      return function(){
        if($(this).hasClass("disabled")) return false;
        t.opts.offset = (t.opts.offset-t.opts.limit >= 0) ? t.opts.offset-t.opts.limit : 0
        t.call();
      }
  }(this))
  this.opts.next.click(
    function(t){
      return function(){
        if($(this).hasClass("disabled")) return false;
        t.opts.offset = t.opts.offset+t.opts.limit
        t.call();
      }
  }(this))
  this.beforeSendFunction = function(){
    this.opts.prev.addClass("disabled ");
    this.opts.next.addClass("disabled ");
    this.opts.loading.removeClass("disabled ");
  }
  this.call = function(){
    url = (this.opts.link+"?offset="+this.opts.offset+"&limit="+this.opts.limit) +
    ((typeof this.opts.order != "undefined" &&  this.opts.order != "") ? "&order="+this.opts.order+"&orderType="+this.opts.orderType : "") +
    ((typeof this.opts.search != "undefined" &&  this.opts.search != "") ? "&q="+this.opts.search : "");
    if(this.opts.extras !== undefined && this.opts.extras !== null && this.opts.extras.length > 0){
      for (var i = 0; i < this.opts.extras.length; i++) {
        url += "&"+Object.keys(this.opts.extras[i])[0]+"="+this.opts.extras[i][Object.keys(this.opts.extras[i])[0]]
      }
    }
    $.ajax({
      url: url,
      type: 'get',
      beforeSend: function(t){ return function(){ t.beforeSendFunction() } }(this),
      success: function(t){ return function (response) {
        t.opts.content.html(response)
        t.opts.prev.removeClass("disabled hidden")
        t.opts.next.removeClass("disabled hidden")
        t.opts.loading.addClass(" disabled")
        if(t.opts.offset <= 0) t.opts.prev.addClass("disabled");
      }}(this),
      error: function (response) {
        console.log(response)

      }
    });
  }
}

pritierSerialize = function(arr){
  _obj = {};
  for (var i = 0; i < arr.length; i++) {
    _obj[arr[i]["name"]] = arr[i]["value"];
  }
  return _obj;
}

formValidate = function(form,extraValidateFunction){
  this.form = form
  this.toCheckItems = function(){
    _arr = new Array();
    form.find("*[data-check]").each(function(t,i){
      _arr.push(this)
    })
    return _arr
  }
  this.errorBox = form.children("*[data-error-message-box]")
  this.errors = Array();
  this.revertErrors = function(){
    this.errors = Array();
    this.errorBox.innerHTML = "";
    this.itarate(this.toCheckItems(),function(head){
      if(head.hasAttribute("data-show-message-box")){
        $((head.getAttribute("data-show-message-box"))).addClass("hidden")
      }
    })
  }
  this.validateEmail = function(email) {
    var re = new RegExp("[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,3}$")
    return re.test(email);
  }
  this.validateNumber = function(number) {
    var re = /^\d+$/
    return re.test(number)
  }
  this.validateText = function(text) {
    var re = /^[a-zA-Z]+$/
    return re.test(text)
  }
  this.makeError = function(i,errorMsg){
    this.errors[this.errors.length] = {
      object: $(i),
      text: $(i).val(),
      error: errorMsg
    }
    if(i.hasAttribute("data-show-message-box")){
      $((i.getAttribute("data-show-message-box"))).removeClass("hidden")
    }else if(i.hasAttribute("data-error-message")){
      if(this.errorBox.innerHTML != "") this.errorBox.innerHTML = i.Attr("data-error-message")
    }
  }
  this.itarate = function(arr,fn){
    if(arr.length > 0){
      head = arr[0];
      tail = arr.splice(1,arr.length)
      fn(head)
      if(tail.length > 0) this.itarate(tail,fn);
    }
  head = arr[0];
  }
  this.checkFunction = function(item){
    switch (true) {
      case (
        head.hasAttribute("noEmpty")
        &&
        parseInt($(head).val().length) == 0
      ):
        this.makeError(head,"noEmpty")
      break;
      case (
        head.hasAttribute("min")
        &&
        parseInt(head.getAttribute("min")) >= $(head).val().length
      ): this.makeError(head,"min")
      break;
      case (
        head.hasAttribute("max")
        &&
        parseInt(head.getAttribute("min")) <= $(head).val().length
      ): this.makeError(head,"max")
      break;
      case (
        head.hasAttribute("onlyNumber")
        &&
        !this.validateNumber($(head).val())
      ): this.makeError(head,"onlyNumber")
      break;
      case (
        head.hasAttribute("onlyText")
        &&
        !this.validateText($(head).val())
      ): this.makeError(head,"onlyText")
      break;
      case (
        head.hasAttribute("isEmail")
        &&
        !this.validateEmail($(head).val())
      ): this.makeError(head,"isEmail")
      break;
    }
  }
  this.check = function(arr){
    this.itarate(arr,function(head){
      if(typeof extraValidateFunction == "function"){
        extraValidateFunction(head,this.checkFunction.bind(this))
      }else{
        this.checkFunction(head)
      }
    })
    console.log(this.errors)
    return (this.errors.length > 0) ? false : true;
  }

  this.revertErrors()
  return this.check(this.toCheckItems())
}

$("*[data-enter-click]").on('keypress', function (e) {
    if(e.which == 13) $($(this).attr("data-enter-click")).click()
});

$(document).on("click",".btn-to", function() {
  if(!$(this).hasClass("disabled")) window.location = $(this).attr("data-to")
})

removeEmpties = function(obj) {
 var newObj = {}
 for (var key in obj) {
   if(obj[key] instanceof Array) {
     newObj[key] = Array();
     for (var i = 0; i < obj[key].length; i++) {
       newObj[key].push(removeEmpties(obj[key][i]))
     }
   }else if(typeof obj[key] === "object") {
     newObj[key] = removeEmpties(obj[key])
   }else if ((obj.hasOwnProperty(key)) && (obj[key] !== null) && (obj[key] !== "") && (obj[key] !== undefined)) {
     newObj[key] = obj[key];
   }
 }
 return newObj
}

function serializeForm(form){
  var obj = {};
  var elements = form.querySelectorAll("input, select, textarea" );
  for( var i = 0; i < elements.length; ++i ) {
      var element = elements[i];
      var name = element.name;
      var value = (element.value === "true") ? true : (element.value === "false") ? false : element.value;

      if( name ) {
          obj[ name ] = value;
      }
  }

  removeEmpties = function(obj) {
   var newObj = {}
   for (var key in obj) {
     if(obj[key] instanceof Array) {
       newObj[key] = Array();
       for (var i = 0; i < obj[key].length; i++) {
         newObj[key].push(removeEmpties(obj[key][i]))
       }
     }else if(typeof obj[key] === "object") {
       newObj[key] = removeEmpties(obj[key])
     }else if ((obj.hasOwnProperty(key)) && (obj[key] !== null) && (obj[key] !== "") && (obj[key] !== undefined)) {
       newObj[key] = obj[key];
     }
   }
   return newObj
  }

  return removeEmpties(obj);
}

pritierObjCombine = function(formObject){

  function getNumberOfArrayVal(string){
      return parseInt(string.replace("[","").replace("]",""))
  }
  function merge_options(obj1,obj2){
    var obj3 = {};
    for (var attrname in obj1) {
      if(Array.isArray(obj1[attrname])){
        obj3[attrname] = (obj3[attrname] === undefined) ? (Array().concat(obj1[attrname])) : obj3[attrname].concat(obj1[attrname]);
      }else{
        obj3[attrname] = (typeof obj1[attrname] == "object" && typeof obj3[attrname] == "object" ) ? merge_options(obj3[attrname],obj1[attrname]) : obj1[attrname];
      }
    }
    for (var attrname in obj2) {
      if(Array.isArray(obj2[attrname])){
        obj3[attrname] = (obj3[attrname] === undefined) ? (Array().concat(obj2[attrname])) : obj3[attrname].concat(obj2[attrname]);
      }else{
        obj3[attrname] = (typeof obj2[attrname] == "object" && typeof obj3[attrname] == "object" ) ? merge_options(obj3[attrname],obj2[attrname]) : obj2[attrname];
      }
    }
    return obj3;
  }

  RunParser = function(formObject){
    function engine(fo){
      var newObj = {};
      var arrayPos = [];
      for (var variable in fo) {
        if (fo.hasOwnProperty(variable)) {
          var is = (variable.indexOf(".") >= 0 && ( variable.indexOf(".") < variable.search(/(\[\d\])/) || variable.search(/(\[\d\])/) < 0 ) )
          ? "obj"
          : (variable.indexOf("]") >= 0 && ( variable.search(/(\[\d\])/) < variable.indexOf(".") || variable.indexOf(".") < 0 ) )
          ? "arr"
          : "var";

          switch (is) {
            case "obj":
              var nameSplitted = variable.split(".");
              var name = nameSplitted[0];
              nameSplitted.shift();
              var innerKey = nameSplitted.join(".");
              newObj[name] = merge_options(newObj[name], { [innerKey] : fo[variable] });
            break;
            case "arr":
              var nameSplitted = variable.split(/(\[\d\])/);
              var number = getNumberOfArrayVal(nameSplitted[1])
              var name = nameSplitted[0];
              nameSplitted.shift();
              var innerKey = "";
              for (var i = 1; i < nameSplitted.length; i++) {
                innerKey += nameSplitted[i] + ( ( (i+1) == nameSplitted.length) ? "" : "["+i+"]");
              }
              if(newObj[name] === undefined){
                newObj[name] = [];
              }
              if(newObj[name][number] === undefined){
                newObj[name].push(engine( { [innerKey] : fo[variable] } ))
              }else{
                newObj[name][number] = Object.assign({},newObj[name][number],engine( { [innerKey] : fo[variable] } ))
              }
            break;
            default:
              if(variable !== "" && variable !== "0"){
                newObj[variable] = fo[variable]
              }else{
                newObj = fo[variable];
              }
            break;
          }
        }
      }
      for (var variable in newObj) {
        if (newObj.hasOwnProperty(variable)) {
          if(Array.isArray(newObj[variable])){
            for (var i = 0; i < newObj[variable].length; i++) {
              newObj[variable][i] = (typeof newObj[variable][i] === "object") ? engine(newObj[variable][i]) : newObj[variable][i]
            }
          }else{
            newObj[variable] = (typeof newObj[variable] === "object") ? engine(newObj[variable]) : newObj[variable]
          }
        }
      }
      return newObj;
    }
    return engine(formObject)
  }

  return new RunParser(formObject);

}

fixCheckers = function(form){
  var obj = {};
  var elements = form.querySelectorAll("input[type=checkbox]" );
  for( var i = 0; i < elements.length; ++i ) {
    elements[i].setAttribute("value",elements[i].checked)
  }
}
