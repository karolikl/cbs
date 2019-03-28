﻿import React, { Component } from "react";
import { render } from 'react-dom'
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import Highcharts from 'highcharts'
import HighchartsReact from 'highcharts-react-official'
import * as actions from '../actions/analysisactions';

class EpicurvebyweekGrid3 extends Component {

  constructor(props) {
    super(props);
  }

  //TODO Implement component and diagrams
  render() {
    var options = Object.assign({}, this.props.options, {title: {text: "Femaile Under Five"}})
    return (
      <HighchartsReact
        highcharts={Highcharts}
        options={ options }
      />
    );
  }

  componentDidMount() {
    this.props.fetchPostsWithRedux("Week", ["FemaleUnderFive"], "WeekGrid3")
  }
}



function mapStateToProps(state){
  return {
  	options: state.epicurvebyweekGrid3
  }
}

function mapDispatchToProps(dispatch) {
  return bindActionCreators(actions, dispatch)
}

export default connect(mapStateToProps, mapDispatchToProps)(EpicurvebyweekGrid3);