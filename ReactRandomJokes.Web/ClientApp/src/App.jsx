import React from 'react';
import { Route, Routes } from 'react-router-dom';
import 'bootstrap/dist/css/bootstrap.min.css';
import './App.css';
import { RJContextComponent } from './RJContextComponent';
import Layout from './Layout'
import Signup from './Signup'
import Home from './Home'
import Login from './Login'
import Logout from './Logout'
import ViewAll from './ViewAll';


class App extends React.Component {


    render() {
        return (
            <>
                <RJContextComponent>
                    <Layout>
                        <Routes>
                            <Route exact path='/' element={<Home />} />
                            <Route exact path='/signup' element={<Signup />} />
                            <Route exact path='/login' element={<Login />} />
                            <Route exact path='/logout' element={<Logout />} />
                            <Route exact path='/viewall' element={<ViewAll />} />
                        </Routes>
                    </Layout>
                </RJContextComponent>
            </>
        );
    }
};

export default App;