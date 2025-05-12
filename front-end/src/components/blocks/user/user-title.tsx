const UserTitle = ({userName}: { userName: string }) => {
    return (
            <span
                className="text-xl font-semibold mb-4"
            >
                {`Welcome, ${userName}!`}
            </span>
    );
};

export { UserTitle };
export default UserTitle;
