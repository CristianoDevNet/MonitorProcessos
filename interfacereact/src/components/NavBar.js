import React from 'react';

const NavBar = (props) => (

    <nav className="navbar navbar-light bg-light">
        <div className="navbar-brand">
            {props.AppName}
        </div>
    </nav>
)

export default NavBar